/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2018/10/30
 * Time: 10:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace alarm
{
	/// <summary>
	/// 根据rawurl进行相应的处理
	/// </summary>
	/// <param name="rawurl">收到数据的url地址</param>
	/// <param name="data">数据</param>
	/// <returns>需要返回的数据</returns>
	public delegate string RecvHandler(String rawurl,String data); 
	
	public class Listener{
		HttpListener _listener;
		RecvHandler On_recv;
		String file="config.ini";
		
		
		/// <summary>
		/// Description of Listener.
		/// <param name="on_recv" type="RecvHandler(String s)">收到数据后要调用的函数委托</param>
		/// <returns>调用委托函数处理处理数据后返回的结果</returns>
		/// </summary>
		public Listener(RecvHandler on_recv){
			FileStream fs = new FileStream(file,FileMode.Open);
			StreamReader sr = new StreamReader(fs);
			_listener=new System.Net.HttpListener();
			On_recv = on_recv;
			_listener.Prefixes.Add(sr.ReadLine().Trim());
			_listener.Start();
			Start();
		}
		
		public void Start(){
			IAsyncResult result = _listener.BeginGetContext(new AsyncCallback(ListenerCallback),_listener);			
		}
				
		void ListenerCallback(IAsyncResult result)
		{
			string responseString="";
		    HttpListener listener = (HttpListener) result.AsyncState;
		    // Call EndGetContext to complete the asynchronous operation.
		    HttpListenerContext context = listener.EndGetContext(result);
		    HttpListenerRequest request = context.Request;
		    Stream istream = request.InputStream;
		    StreamReader sr =new StreamReader(istream,Encoding.UTF8);
		    String rcv_json = sr.ReadToEnd();
		    save2file(rcv_json);
		    sr.Close();
		    istream.Close();
		    if(this.On_recv!=null){
		    	// Construct a response.
		    	responseString = this.On_recv(request.RawUrl, rcv_json);
		    }
		    // Obtain a response object.
		    HttpListenerResponse response = context.Response;
		    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
		    // Get a response stream and write the response to it.
		    response.ContentLength64 = buffer.Length;
		    System.IO.Stream output = response.OutputStream;
		    output.Write(buffer,0,buffer.Length);
		    // You must close the output stream.
		    output.Close();
		    
		    this.Start();
		}
		
		void save2file(String jsoncode){
			FileStream fs =new FileStream("recv_json.txt",FileMode.Append);
			StreamWriter sw = new StreamWriter(fs,Encoding.GetEncoding("gb2312"));
			sw.Write(jsoncode);
			sw.Close();
			fs.Close();
		
		}
		
		
//		~Listener(){
//			this._listener.Stop();
//		}
	} 
}
