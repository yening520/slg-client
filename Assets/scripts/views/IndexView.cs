﻿using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

public class IndexView : MonoBehaviour
{
		private bool loadIndex = false;
		private bool started = false;
		public User user = null;

		public void Start ()
		{
				started = true;

		}

		public void OnGUI ()
		{
				if (!started)
						return;
				if (!loadIndex) {
						Command cmd = new Command ("user", "getInfo", new Dictionary<string,object> ());
						SlgDispatcher dispatcher = SlgDispatcher.Instance;
						dispatcher.send (cmd);
						Command cmd2 = new Command ("role", "userRoleList", new Dictionary<string,object> ());
						dispatcher.send (cmd2);
						loadIndex = true;

						Dictionary<string,object> dic = new Dictionary<string,object> ();
						dic.Add ("type", "weapon");
						cmd = new Command ("equip", "noUsedEquipList", dic);
						dispatcher.send (cmd);



						dic.Clear ();
						dic = new Dictionary<string,object> ();
						dic.Add ("type", "armor");
						cmd = new Command ("equip", "noUsedEquipList", dic);
						dispatcher.send (cmd);


						dic.Clear ();
						dic = new Dictionary<string,object> ();
						dic.Add ("type", "accessory");
						cmd = new Command ("equip", "noUsedEquipList", dic);
						dispatcher.send (cmd);
				}

				if (user != null) {
						// show user info
						GUI.Label (new Rect (20, 20, 80, 20), "id:" + user.id);
						GUI.Label (new Rect (20, 40, 80, 20), "粮食:" + user.food);
						GUI.Label (new Rect (20, 60, 80, 20), "元宝:" + user.cash);
						GUI.Label (new Rect (20, 80, 80, 20), "当前经验:" + user.xp);
						GUI.Label (new Rect (20, 100, 80, 20), "金币:" + user.gold);
						GUI.Label (new Rect (20, 120, 80, 20), "战斗力:" + user.fightForce);
						GUI.Label (new Rect (20, 140, 80, 20), "等级:" + user.level);
						GUI.Label (new Rect (20, 160, 80, 20), "名称:" + user.name);
	

						CreateFunctionalButton ();
			
				}
						


		}

		public void CreateFunctionalButton ()
		{
				// left hand buttons 
				// include getGold,myRoleList,myPackage,tavern,zuling
				if (GUI.Button (new Rect (20, 180, 80, 20), "收获金币")) {
						Command cmd = new Command ("castle", "harvest", new Dictionary<string,object> ());
						SlgDispatcher dispatcher = SlgDispatcher.Instance;
						dispatcher.send (cmd);
				}

				if (GUI.Button (new Rect (20, 200, 80, 20), "我的将领")) {
						Application.LoadLevel ("roleList");
				}

				if (GUI.Button (new Rect (20, 220, 80, 20), "我的背包")) {
						Application.LoadLevel ("package");
				}

				if (GUI.Button (new Rect (20, 240, 80, 20), "酒馆")) {
						Application.LoadLevel ("tavern");
				}
				if (GUI.Button (new Rect (20, 260, 80, 20), "祖灵地之旅")) {
						Application.LoadLevel ("zuling");
				}


				//  right hand buttons 
				// include getGold,shop,team,fight
				if (GUI.Button (new Rect (110, 180, 80, 20), "收获粮食")) {
						Command cmd = new Command ("farm", "harvest", new Dictionary<string,object> ());
						SlgDispatcher dispatcher = SlgDispatcher.Instance;
						dispatcher.send (cmd);
				}

				if (GUI.Button (new Rect (110, 200, 80, 20), "商店")) {
						Application.LoadLevel ("shop");
				}

				if (GUI.Button (new Rect (110, 220, 80, 20), "阵容")) {
						Application.LoadLevel ("team");
				}

				if (GUI.Button (new Rect (110, 240, 80, 20), "战斗")) {
						Application.LoadLevel ("fight");
				}

	
		}
	
	
}
