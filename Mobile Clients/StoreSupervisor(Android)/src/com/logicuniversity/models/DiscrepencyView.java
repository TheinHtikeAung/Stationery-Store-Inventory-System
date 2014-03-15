package com.logicuniversity.models;

import java.util.HashMap;



public class DiscrepencyView extends HashMap<String, String> {
	public int id;
    public int stationery_id;
    public String stationery_name;
    public int quantity;
    public String remark;
    public String datetime_reported;
    public double price;
    public double totalprice;
}

