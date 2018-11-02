/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2018/11/2
 * Time: 8:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace alarm
{
	/// <summary>
	/// Description of Group.
	/// </summary>
	public class Group
	{
		//用来加载到listview中的items;
		public ListView.ListViewItemCollection listviewitems;
		//
		Dictionary<String,ListViewItem> dic_instance_item;
		public String name;
		/// <summary>
		/// alarm组
		/// </summary>
		/// <param name="groupname">组名</param>
		public Group(String groupname,ListView owner){
			name=groupname;
			dic_instance_item =new Dictionary<string, ListViewItem>();
			listviewitems = new ListView.ListViewItemCollection(owner);
			
		}
		
		public void AddItem(Alarm alarm){
			if(!dic_instance_item.ContainsKey(alarm.instance)){
				dic_instance_item.Add(alarm.instance,CreateListViewItem(alarm));
				//Add the items to the ListView.
				listviewitems.Add(dic_instance_item[alarm.instance]);
			   }
		}
		
		public void RemoveItem(Alarm alarm){
			if(!dic_instance_item.ContainsKey(alarm.instance)){
				return;
			}
			listviewitems.Remove(dic_instance_item[alarm.instance]);
			dic_instance_item.Remove(alarm.instance);
		}
				
		private ListViewItem CreateListViewItem(Alarm a){

			
			ListViewItem item = new ListViewItem(a.instance.Trim());
			//item.Checked = true;
			item.SubItems.Add(a.group.Trim());
			item.SubItems.Add(a.type.Trim());
			item.SubItems.Add(a.startsAt.Trim());
			item.SubItems.Add(a.summary.Trim());
			return item;
		}
		
		public int Count{
			get{ return dic_instance_item.Count; }
		}
		
	}
}
