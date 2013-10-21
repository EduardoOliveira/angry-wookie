using UnityEngine;
using System.Collections;
using System;

public class MessageBuilder {

    private static readonly int HEADER_SIZE = 2;
    
    private byte type;
	private byte[] content=null;

	public MessageBuilder(byte type, byte[] content){
		this.type = type;
		this.content = content;
	}

	public MessageBuilder(byte type){
		this.type=type;
		this.content = new byte[0];
	}

    //** Single Variable
    public MessageBuilder Add(string newContent)
    {
        this.Add(System.Text.Encoding.UTF8.GetBytes(newContent));
        return this;
    }

	public MessageBuilder Add(int newContent){
		byte[] toInvert = BitConverter.GetBytes(newContent);
		if (BitConverter.IsLittleEndian)
            Array.Reverse(toInvert);
        this.Add(toInvert);
		return this;
	}

	public MessageBuilder Add(float newContent){
		byte[] toInvert = BitConverter.GetBytes(newContent);
		if (BitConverter.IsLittleEndian)
            Array.Reverse(toInvert);
        this.Add(toInvert);
		return this;
	}

    public MessageBuilder Add(bool newContent)
    {
        this.Add(new byte[] {Convert.ToByte(newContent)});
        return this;
    }

    public MessageBuilder Add(byte newContent) 
    {
        this.Add(new byte[]{newContent});
        return this;
    }
    //** Single Variable

    public MessageBuilder Add(byte[] newContent)
    {
        if (this.content == null)
        {
            this.content = new byte[newContent.Length];
            Buffer.BlockCopy(newContent, 0, this.content, 0, newContent.Length);
        }
        else
        {
            byte[] temp = new byte[this.content.Length + newContent.Length];
            Buffer.BlockCopy(this.content, 0, temp, 0, this.content.Length);
            Buffer.BlockCopy(newContent, 0, temp, this.content.Length, newContent.Length);
            this.content = temp;
        }
        return this;
    }

	public byte[] Build(){
        int size = HEADER_SIZE + this.content.Length;
		byte[] message = new byte[size];
        message[0] = (byte)(size - 1);
        message[1] = this.type;
        Buffer.BlockCopy(this.content, 0, message, MessageBuilder.HEADER_SIZE, this.content.Length);
		return message;
	}

}
