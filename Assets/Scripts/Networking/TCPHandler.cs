using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;


public class TCPHandler{
	private static TCPHandler instance = null;
	private string server = "172.17.13.77";
	private int port = 30001;
    private Socket socket;
    private Thread listener = null;
	private object lock_var = new object();
	
	public void connect(string server, string port){
		if(server!=null)
			this.server = server;
		if(port!=null)
			this.port = System.Int32.Parse(port);
		
		IPHostEntry ipHostInfo = Dns.Resolve(this.server);
		IPAddress ipAddress = ipHostInfo.AddressList[0];
		IPEndPoint remoteEP = new IPEndPoint(ipAddress,this.port);
		
		socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp );
		Console.Write(">Connecting...");
		try {
			socket.Connect(remoteEP);
			Console.Write(">Connected");
		}catch(SocketException e){
			Debug.Log(e.ToString());
		}
        listener = new Thread(receive);
        listener.Start();
	}
	
	public void send(byte[] data) 
    {
		try
        {
            lock (this.lock_var)
            {
                socket.Send(data);
            }
		}catch(SocketException e){
			Console.Write(e.ToString());
		}
    }

    private void receive()
    {
        try
        {
            
            byte[] bytes;
            int bytesRec;
            byte[] msg;
            while (true)
            {
                int index = 0;
                bytes = new byte[1024];
                Console.Write("before read");
                bytesRec = socket.Receive(bytes);
                Console.Write("Read " + bytes.ToString());
                if (bytesRec != -1)
                {
                   int size = bytes[index];
                   index++;
                   msg = new byte[size];
                   Console.Write("Size"+size);
                   Buffer.BlockCopy(bytes, index, msg, 0, size);
                   Message message = new Message(msg);
                   MessageDispatcher.Dispatch(message);
                   index += size + 1;
                }
                else
                {
                    throw new SocketException();
                }
            }
        }
        catch (ThreadInterruptedException e)
        {
            Debug.Log(e.ToString());
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }


	public static TCPHandler getInstance(){
		if(instance == null){
			instance = new TCPHandler();
		}
		return instance;
	}
	
	private TCPHandler(){
		connect(server,port+"");
	}

    public void stop()
    {
        socket.Close();
    }
}
