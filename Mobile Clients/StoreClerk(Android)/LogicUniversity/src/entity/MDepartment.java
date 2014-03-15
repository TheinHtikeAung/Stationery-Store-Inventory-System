package entity;

import java.util.HashMap;

public class MDepartment extends HashMap<String, String>{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	
	public int id;
	public String department_name;
	public String representative_name;
	public String name;
	
	public MDepartment(int id, String department_name,
			String representative_name, String name) {		
		
		this.id=id;
		this.department_name=department_name;
		this.representative_name=representative_name;
		this.name=name;
		
		put("id", Integer.toString(id));
		put("department_name", department_name);
		put("representative_name", representative_name);
		put("name",name);
	}
}
