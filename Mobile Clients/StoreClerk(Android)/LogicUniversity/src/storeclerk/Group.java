package storeclerk;

import java.util.ArrayList;
import java.util.List;

import entity.MRetrievalFormDetails_View;
import entity.MRetrievalFormMaster_View;

public class Group {
	public MRetrievalFormMaster_View master;
	public final List<MRetrievalFormDetails_View> details=new ArrayList<MRetrievalFormDetails_View>();
	
	public Group(MRetrievalFormMaster_View master)
	{
		this.master=master;
	}
	
}
