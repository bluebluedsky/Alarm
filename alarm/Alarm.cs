/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2018/11/1
 * Time: 14:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace alarm
{
			/*# Prometheus webhook 告警json 的格式,"alert"数组存放的是 警报数组
			# {
			#   "version": "4",
			#   "groupKey": <string>,    // key identifying the group of alerts (e.g. to deduplicate)
			#   "status": "<resolved|firing>",
			#   "receiver": <string>,
			#   "groupLabels": <object>,
			#   "commonLabels": <object>,
			#   "commonAnnotations": <object>,
			#   "externalURL": <string>,  // backlink to the Alertmanager.
			#   "alerts": [
			#     {
			#       "status": "<resolved|firing>",
			#       "labels": <object>,
			#       "annotations": <object>,
			#       "startsAt": "<rfc3339>",
			#       "endsAt": "<rfc3339>",
			#       "generatorURL": <string> // identifies the entity that caused the alert
			#     },
			#     ...
			#   ]
			# }
			*/
	
	/// <summary>
	/// Description of Alarm.
	/// </summary>
	public class Alarm
	{
		public string status;
		public string startsAt;
		public string alertname;
		public string type;
		public string group;
		public string instance;
		public string summary;
		
		public Alarm(){
			
		}
		public Alarm(JObject jo){
			status=(String)(jo.GetValue("status"));
			startsAt=(String)(jo.GetValue("startsAt"));
			JObject labels=(JObject)jo.GetValue("labels");
			JObject annotations=(JObject)jo.GetValue("annotations");
			alertname=(String)(labels.GetValue("alertname"));
			type=(String)(labels.GetValue("type"));
			group=(String)(labels.GetValue("group"));
			instance=(String)(labels.GetValue("instance"));
			summary=(String)(annotations.GetValue("summary"));
		}
		public override string ToString()
		{
			return string.Format("[Alarm Status={0}, StartsAt={1}, Alertname={2}, Type={3}, Group={4}, Instance={5}, Summary={6}]", status, startsAt, alertname, type, group, instance, summary);
		}

	}
}
