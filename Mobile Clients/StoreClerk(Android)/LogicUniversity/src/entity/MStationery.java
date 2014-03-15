package entity;

import java.util.HashMap;

public class MStationery extends HashMap<String, String>{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	
	public int id;
	public String stationery_name;
	
	public MStationery(int id, String stationery_name)
	{
		this.id=id;
		this.stationery_name=stationery_name;
		
		put("id", String.valueOf(id));
		put("stationery_name", stationery_name);
		
	}
}
