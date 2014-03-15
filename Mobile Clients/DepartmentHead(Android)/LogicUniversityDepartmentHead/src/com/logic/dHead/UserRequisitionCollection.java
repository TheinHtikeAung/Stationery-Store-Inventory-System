package com.logic.dHead;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class UserRequisitionCollection {
	public ArrayList<String> employees = new ArrayList<String>();
   public  HashMap<String,ArrayList<Requisition>> requisitionCollection = new HashMap<String,ArrayList<Requisition>>();
public ArrayList<String> getEmployees() {
	return employees;
}
public void setEmployees(ArrayList<String> employees) {
	this.employees = employees;
}
public HashMap<String, ArrayList<Requisition>> getRequisitionCollection() {
	return requisitionCollection;
}
public void setRequisitionCollection(
		HashMap<String, ArrayList<Requisition>> requisitionCollection) {
	this.requisitionCollection = requisitionCollection;
}
   
   
}
