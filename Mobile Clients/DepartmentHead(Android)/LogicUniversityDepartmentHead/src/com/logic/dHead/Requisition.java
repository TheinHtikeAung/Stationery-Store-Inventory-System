package com.logic.dHead;



public class Requisition {

	private int reqId,quantity;
	private String stationaryName;
	private String requisitionDate;
	private String rejectionReason;
	private String employeeName;
	
	public String getEmployeeName() {
		return employeeName;
	}
	public void setEmployeeName(String employeeName) {
		this.employeeName = employeeName;
	}
	public String getRejectionReason() {
		return rejectionReason;
	}
	public void setRejectionReason(String rejectionReason) {
		this.rejectionReason = rejectionReason;
	}
	public String getRequisitionDate() {
		return requisitionDate;
	}
	public void setRequisitionDate(String requisitionDate) {
		this.requisitionDate = requisitionDate;
	}
	public int getReqId() {
		return reqId;
	}
	public void setReqId(int reqId) {
		this.reqId = reqId;
	}
	public int getQuantity() {
		return quantity;
	}
	public void setQuantity(int quantity) {
		this.quantity = quantity;
	}
	public String getStationaryName() {
		return stationaryName;
	}
	public void setStationaryName(String stationaryName) {
		this.stationaryName = stationaryName;
	}
	
	
}
