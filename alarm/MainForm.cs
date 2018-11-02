/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2018/10/30
 * Time: 10:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace alarm
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		//http监听
		Listener listener;
		
		//界面部分
		//每个标签页对应的listview
		Dictionary<String ,AlarmPage> dic_alarmpage;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			dic_alarmpage =new Dictionary<string, AlarmPage>();
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			//CreateMyListView();
			listener=new Listener(new RecvHandler(On_recv));
			listener.Start();
		}
		
		/// <summary>
		/// 接收从端口收到prometheus发来的数据
		/// </summary>
		/// <param name="rawurl"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		string On_recv(String rawurl,String jsoncode){
			string[] args = new string[2];
			args[0]=rawurl;
			args[1]= jsoncode;
			this.BeginInvoke(new RecvHandler(On_Rcv),args);
			return "";
		}
		
		
		//更新alarmpage字典
		public string On_Rcv(String rawurl,String jsoncode){
			AlarmPage cur_alarmpage = null;
			JArray ja= (JArray)JObject.Parse(jsoncode).GetValue("alerts");
			foreach(JObject jo in ja){
				Alarm alarm = new Alarm(jo);
				//tbx_info
				this.tbx_info.AppendText(alarm.ToString()+"\r\n");
				//判断是否存在标签页
				if(!dic_alarmpage.ContainsKey(alarm.alertname)){
					dic_alarmpage.Add(alarm.alertname,new AlarmPage(alarm.alertname));
					this.tabControl1.Controls.Add(dic_alarmpage[alarm.alertname].page);
				}
				cur_alarmpage = dic_alarmpage[alarm.alertname];
				if(alarm.status=="firing"){
					cur_alarmpage.Additem(alarm);	
					this.WindowState = FormWindowState.Normal;
					this.Activate();
				}else if(alarm.status=="resolved"){
					cur_alarmpage.Removeitem(alarm);
					if(cur_alarmpage.Count==0){
						this.tabControl1.Controls.Remove(cur_alarmpage.page);
						dic_alarmpage.Remove(alarm.alertname);
					}
				}
			}
			
			//更新状态栏
			Update_status();
			
			return "";
		}
		
		void 文件ToolStripMenuItemClick(object sender, EventArgs e)
		{
			
			
		}	
		
		//更新status 栏
		void Update_status(){
			this.StatusLabel_UpdateTime.Text= DateTime.Now.ToString();
		}
	}
}
