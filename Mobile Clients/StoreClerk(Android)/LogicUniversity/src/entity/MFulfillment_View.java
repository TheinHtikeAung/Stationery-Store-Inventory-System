package entity;

import java.io.Serializable;
import java.util.HashMap;

import android.os.Parcel;
import android.os.Parcelable;

public class MFulfillment_View extends HashMap<String, String> implements Parcelable{

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	public int id;
	public int department;
	public int stationery;
	public String stationery_name;
	public int request_quantity;
	public int fulfill_quantity;
	public int unfulfill_quantity;
	public int stock_control;
	
	public MFulfillment_View(int id, int department, int statoinery, String stationery_name, int request_quantity,
			int fulfill_quantity, int unfulfill_quantity, int stock_control)
	{
		this.id=id;
		this.department=department;
		this.stationery=statoinery;
		this.stationery_name=stationery_name;
		this.request_quantity=request_quantity;
		this.fulfill_quantity=fulfill_quantity;
		this.unfulfill_quantity=unfulfill_quantity;
		this.stock_control=stock_control;
		
		put("id",Integer.toString(id));
		put("department",Integer.toString(department));
		put("stationery",Integer.toString(statoinery));
		put("stationery_name",stationery_name);
		put("requested_quantity",Integer.toString(request_quantity));
		put("fulfill_quantity",Integer.toString(fulfill_quantity));
		put("unfulfill_quantity",Integer.toString(unfulfill_quantity));
		put("stock_control",Integer.toString(stock_control));
	}
	
	public MFulfillment_View(Parcel in){
		readFromParcel(in);
	}
	
	

	@Override
	public int describeContents() {
		// TODO Auto-generated method stub
		return 0;
	}

	@Override
	public void writeToParcel(Parcel dest, int flags) {

		dest.writeInt(id);
		dest.writeInt(department);
		dest.writeInt(stationery);
		dest.writeString(stationery_name);
		dest.writeInt(request_quantity);
		dest.writeInt(fulfill_quantity);
		dest.writeInt(unfulfill_quantity);
		dest.writeInt(stock_control);
		
	}
	
	private void readFromParcel(Parcel in ) {

        this.id=in.readInt();
		this.department=in.readInt();
		this.stationery=in.readInt();
		this.stationery_name=in.readString();
		this.request_quantity=in.readInt();
		this.fulfill_quantity=in.readInt();
		this.unfulfill_quantity=in.readInt();
		this.stock_control=in.readInt();
    }
}
