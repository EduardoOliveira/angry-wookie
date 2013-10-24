using UnityEngine;
using System.Collections;
using System;
using System.Text;

public class Message{
    private byte[] content = null;
    private int pointer = 1;
    public Message(byte[] content)
    {
        this.content = content;
    }

    internal byte getOpCode()
    {
        return content[0];
    }

    public int getNextInt()
    {
        byte[] integer = new byte[sizeof(int)];
        Buffer.BlockCopy(content, pointer, integer, 0, sizeof(int));
        if (BitConverter.IsLittleEndian)
            Array.Reverse(integer);
        int rtn = BitConverter.ToInt32(integer, 0);
        pointer += sizeof(int);
        return rtn;
    }

    public float getNextFloat()
    {
		
        byte[] f = new byte[sizeof(float)];
        Buffer.BlockCopy(content, pointer, f, 0, sizeof(float));
        if (BitConverter.IsLittleEndian)
            Array.Reverse(f);
        float rtn = BitConverter.ToSingle(f, 0);
        pointer += sizeof(float);
        return rtn;
    }


    public string getNextString()
    {
        int size = getNextInt();
        string rtn = Encoding.UTF8.GetString(content, pointer, size);
        pointer += size;
        return rtn;
    }

    public Vector3 getNextVector3() 
    {
        return new Vector3(getNextFloat(), getNextFloat(), getNextFloat());
    }

    public Quaternion getNextQuartenion()
    {
        return new Quaternion(getNextFloat(), getNextFloat(), getNextFloat(), getNextFloat());
    }

    public string printBytes() 
    {
        StringBuilder hex = new StringBuilder(content.Length * 2);
        foreach (byte b in content)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }

}
