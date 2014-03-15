package entity;

import java.util.HashMap;

public class MRetrievalFormMaster_View extends HashMap<String, String>{
	
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	public int id;
	public String stationery_name;
	public int Needed;
	public int Retrieved;
	
	public MRetrievalFormMaster_View(int id, String stationer_name, int Needed, int Retrieved)
	{
		this.id=id;
		this.stationery_name=stationer_name;
		this.Needed=Needed;
		this.Retrieved=Retrieved;
		
		put("id", Integer.toString(id));
		put("stationery_name", stationer_name);
		put("Needed", Integer.toString(Needed));
		put("Retrieved",Integer.toString(Retrieved));
	}
}
