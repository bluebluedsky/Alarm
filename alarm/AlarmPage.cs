/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2018/11/2
 * Time: 8:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace alarm
{
	/// <summary>
	/// 存放tabpage，Alarm Group数据的类
	/// </summary>
	public class AlarmPage
	{
		//group名称和对应的alarm数据
		public Dictionary<String,Group> dic_group;
		//标签页 tabpage
		public TabPage page;
		//tabpage 对应的listview
		public ListView ListView;
		public AlarmPage(String alertname)		{
			page = CreateTabPage(alertname);
			ListView = CreateListView();
			page.Controls.Add(ListView);
			dic_group = new Dictionary<String, Group>();
		}
		
		//添加告警信息
		public void Additem(Alarm alarm){
			//检查分组
			if(!dic_group.ContainsKey(alarm.group)){
				dic_group.Add(alarm.group,new Group(alarm.group,ListView));
			}
			dic_group[alarm.group].AddItem(alarm);
		}
		
		//移除告警信息
		public void Removeitem(Alarm alarm){
			//检查分组
			if(!dic_group.ContainsKey(alarm.group)){
				return;
			}
			dic_group[alarm.group].RemoveItem(alarm);
			if(dic_group[alarm.group].Count==0){
				dic_group.Remove(alarm.group);
			}
		}
		
		private TabPage CreateTabPage(String alertname){
			TabPage cur_tabpage = new TabPage();
			cur_tabpage.Location = new System.Drawing.Point(4, 22);
			cur_tabpage.Name = alertname;
			cur_tabpage.Padding = new System.Windows.Forms.Padding(3);
			cur_tabpage.Size = new System.Drawing.Size(997, 294);
			cur_tabpage.TabIndex = 0;
			cur_tabpage.Text = alertname;
			cur_tabpage.UseVisualStyleBackColor = true;
			return cur_tabpage;
			
			
			
			
//			this.tabPage2.Size = new System.Drawing.Size(997, 294);
//			this.tabPage2.TabIndex = 1;
//			this.tabPage2.Text = "tabPage2";
//			this.tabPage2.UseVisualStyleBackColor = true;
		}
		
		private ListView CreateListView(){
			ListView cur_listview =new ListView();
			cur_listview.Dock = DockStyle.Fill;
			cur_listview.Location = new System.Drawing.Point(3, 3);
			cur_listview.Size = new System.Drawing.Size(991, 288);
			cur_listview.View = View.Details;
			cur_listview.LabelEdit = true;
			cur_listview.AllowColumnReorder = true;
			//cur_listview.CheckBoxes = true;
			cur_listview.FullRowSelect = true;
			cur_listview.GridLines = true;
			cur_listview.Sorting = SortOrder.Ascending;
		          			
			// Create columns for the items and subitems.
			// Width of -2 indicates auto-size.
			
			cur_listview.Columns.Add("名称", 160, HorizontalAlignment.Left);
			cur_listview.Columns.Add("分组", 180, HorizontalAlignment.Left);
			cur_listview.Columns.Add("类型", 80, HorizontalAlignment.Left);
			cur_listview.Columns.Add("时间", 200, HorizontalAlignment.Left);
			cur_listview.Columns.Add("摘要", -2, HorizontalAlignment.Center);
			return cur_listview;
		}
		
		public int Count{
			get{ return dic_group.Count; }
		}

	}
}
