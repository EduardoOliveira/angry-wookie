using UnityEngine;
using System.Collections;

public class MessageType {

	/*
	 * Login Messages
	 */
    
	public const byte LOGIN = 0x10;				    //client basic login auth token trade
	public const byte LOGIN_INFO = 0x11; 			//server character info
	public const byte LOGIN_SUCCESSFUL = 0x12;	    //client login_info success server login success
	public const byte LOGIN_FAILURE = 0x13;		    //client login_info failure server login general failure
	public const byte LOGOUT = 0x14;				//client batata

    /*
     * Player Messages
     */

    public const byte MOVEMENT_SYNC = 0x20;         //player move message

}
