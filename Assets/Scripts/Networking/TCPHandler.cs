using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;


public class TCPHandler{
	private static TCPHandler instance = null;
	private string server = "169.254.191.224";
	private int port = 30001;
    private Socket socket;
    private Thread listener = null;
	
	
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
			Console.Write(e.ToString());
		}
        listener = new Thread(receive);
        listener.Start();
	}
	
	public void send(byte[] data) {
		try{
			socket.Send(data);
		}catch(SocketException e){
			Console.Write(e.ToString());
		}
    }

    private void receive()
    {
        try
        {
            int index = 0;
            byte[] bytes;
            int bytesRec;
            byte[] msg;
            while (true)
            {
                bytes = new byte[1024];
                bytesRec = socket.Receive(bytes);
                if (bytesRec != -1)
                {

                   int size = bytes[index];
                   index++;
                   msg = new byte[size];
                   Console.Write("Size"+size);
                   Buffer.BlockCopy(bytes, index, msg, 0, size);
                   Message message = new Message(msg);
                   Console.Write(message.printBytes());
                   //Console.Write(message.getNextString());
                   MessageDispatcher.Dispatch(message);
                   index += size + 1;
                    /*foreach(byte b in bytes)
                    {
                       
                        
                       Console.Write(b+ "");
                    }*/
                    
                    //string message = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    //Console.Write(">"+message);
                }
                else
                {
                    throw new SocketException();
                }
            }
        }
        catch (ThreadInterruptedException e)
        {
            Console.Write(e.ToString());
        }
        catch (Exception e)
        {
            Console.Write(e.ToString());
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
