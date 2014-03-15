package com.logic.dHead;
import java.util.ArrayList;
public class UserRequisition {

	private int userId;
	private String employeeName;
	public ArrayList<Requisition> userRequisitions = new ArrayList<Requisition>();
	public int getUserId() {
		return userId;
	}
	public void setUserId(int userId) {
		this.userId = userId;
	}
	public String getEmployeeName() {
		return employeeName;
	}
	public void setEmployeeName(String employeeName) {
		this.employeeName = employeeName;
	}
	public ArrayList<Requisition> getUserRequisitions() {
		return userRequisitions;
	}
	public void setUserRequisitions(ArrayList<Requisition> userRequisitions) {
		this.userRequisitions = userRequisitions;
	}
	
}
