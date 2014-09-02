﻿using UnityEngine;
using System.Collections;
using LitJson;

public class RoleService : MonoBehaviour {

	public WearEquipView wearEquipView ;

	public void userRoleListHandler(JsonData jsonData)
	{
		JsonData roleLists = jsonData ["data"] ["list"];
		Debug.Log (roleLists.Count);
		User user = Singleton.getInstance (SingletonConstants.VO.USER) as User;
		user.userRolesMap = new Hashtable ();
		user.equipMap = new Hashtable ();
		Debug.Log ("role list count = " + roleLists.Count);
		for(int i=0;i<roleLists.Count;i++)
		{
			JsonData item = roleLists[i];
			UserRole ur = new UserRole();
			ur.accessory = int.Parse(item["accessory"].ToString()); 
			ur.armor = int.Parse(item["armor"].ToString()); 
			ur.weapon = int.Parse(item["weapon"].ToString()); 
			ur.attack = int.Parse(item["attack"].ToString()); 
			ur.defence = int.Parse(item["defence"].ToString()); 
			ur.health = int.Parse(item["health"].ToString()); 
			ur.fightForce = int.Parse(item["fightForce"].ToString()); 
			ur.id = int.Parse(item["id"].ToString()); 
			ur.roleId = int.Parse(item["roleId"].ToString());
			ur.roleName = item["roleName"].ToString();
			ur.level = int.Parse(item["level"].ToString());
			user.userRolesMap.Add (ur.id,ur);
			Debug.Log ("add 1");
			if(ur.accessory!=0)
			{
				JsonData accessoryInfo= item["accessoryInfo"];
				addEquip2Hashtable(user.equipMap,accessoryInfo);
			}
			if(ur.weapon!=0)
			{
				JsonData weaponInfo= item["weaponInfo"];
				addEquip2Hashtable(user.equipMap,weaponInfo);
			}
			if(ur.armor!=0)
			{
				JsonData armorInfo= item["armorInfo"];
				addEquip2Hashtable(user.equipMap,armorInfo);
			}

			try{
				JsonData putongSkill = item["putongRoleSkill"];
				RoleSkill rs = new RoleSkill();
				rs.id = int.Parse(putongSkill["id"].ToString());
				rs.level =int.Parse( putongSkill["level"].ToString());
				rs.name = putongSkill["name"].ToString();
				rs.rid = int.Parse(putongSkill["rid"].ToString());
				rs.rsid = int.Parse(putongSkill["rsid"].ToString());
				rs.type = putongSkill["type"].ToString();
				ur.putong = rs;
			}catch(System.Collections.Generic.KeyNotFoundException e){}
			
			try{
			JsonData tianfuRoleSkill = item["tianfuRoleSkill"];
				RoleSkill rs = new RoleSkill();
				rs.id = int.Parse(tianfuRoleSkill["id"].ToString());
				rs.level =int.Parse( tianfuRoleSkill["level"].ToString());
				rs.name = tianfuRoleSkill["name"].ToString();
				rs.rid = int.Parse(tianfuRoleSkill["rid"].ToString());
				rs.rsid = int.Parse(tianfuRoleSkill["rsid"].ToString());
				rs.type = tianfuRoleSkill["type"].ToString();
				ur.tianfu = rs;
			}catch(System.Collections.Generic.KeyNotFoundException e){

			}


		}

	}

	private void addEquip2Hashtable(Hashtable equipMap,JsonData jsonData)
	{
		Equipment e = new Equipment ();
		e.id = int.Parse (jsonData["userEquipId"].ToString());
		e.name = jsonData ["name"].ToString ();
		e.level = int.Parse (jsonData ["level"].ToString ());
		equipMap.Add (e.id, e);
	}

	public void wearHandler(JsonData jsonData){
		int urid = int.Parse (jsonData ["args"] ["urid"].ToString ());
		int ueid = int.Parse(jsonData["args"]["ueid"].ToString());
		User user = Singleton.getInstance (SingletonConstants.VO.USER) as User;
		UserRole currentRole = null;
		// find user role
		currentRole = (UserRole)user.userRolesMap [urid];
		Equipment e = (Equipment)user.noUsedEquipMap[ueid];
		if(e.type.Equals("weapon"))
		{
			currentRole.weapon = e.id;
		}
		if(e.type.Equals("armor"))
		{
			currentRole.armor = e.id;
		}
		if(e.type.Equals("accessory"))
		{
			currentRole.accessory = e.id;
		}
		user.noUsedEquipMap.Remove (e.id);
		user.equipMap.Add (e.id, e);

		wearEquipView.jump ();
	}


}
