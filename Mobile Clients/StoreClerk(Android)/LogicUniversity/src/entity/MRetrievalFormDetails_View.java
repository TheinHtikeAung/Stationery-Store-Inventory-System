package entity;

import java.util.HashMap;

public class MRetrievalFormDetails_View extends HashMap<String, String>{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	
	public int id;
	public String department_name;
	public int Needed;
	public int Actual;
	public int stationery;
	
	public MRetrievalFormDetails_View(int id, String department_name,
			int needed, int actual, int stationery) {
	
		this.id = id;
		this.department_name = department_name;
		Needed = needed;
		Actual = actual;
		this.stationery = stationery;
		
		put("id", Integer.toString(id));
		put("department_name", department_name);
		put("Needed", Integer.toString(Needed));
		put("Actual",Integer.toString(Actual));
		put("stationery",Integer.toString(Actual));
	}
	
	
	
}
