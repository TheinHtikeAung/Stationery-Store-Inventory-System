package storeclerk;

import com.logicuniversity.R;
import com.logicuniversity.Util;

import entity.MRetrievalFormDetails_View;
import android.R.anim;
import android.app.Activity;
import android.util.SparseArray;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.widget.BaseExpandableListAdapter;
import android.widget.CheckedTextView;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

public class MyExpandableListAdapter extends BaseExpandableListAdapter{
	private final SparseArray<Group> groups;
	  public LayoutInflater inflater;
	  public Activity activity;

	  public MyExpandableListAdapter(Activity act, SparseArray<Group> groups) {
	    activity = act;
	    this.groups = groups;
	    inflater = act.getLayoutInflater();
	  }

	  @Override
	  public Object getChild(int groupPosition, int childPosition) {
	    return groups.get(groupPosition).details.get(childPosition);
	  }

	  @Override
	  public long getChildId(int groupPosition, int childPosition) {
	    return 0;
	  }

	  @Override
	  public View getChildView(int groupPosition, final int childPosition,
	      boolean isLastChild, View convertView, ViewGroup parent) {
	    //final String children = (String) getChild(groupPosition, childPosition);
		  
		final MRetrievalFormDetails_View d = (MRetrievalFormDetails_View) getChild(groupPosition, childPosition);
	    TextView text = null;
	    if (convertView == null) {
	      convertView = inflater.inflate(R.layout.listrow_details, null);
	    }
	    
	    text = (TextView) convertView.findViewById(R.id.txtDeptName);
	    text.setText(d.department_name);
	    
	    text = (TextView) convertView.findViewById(R.id.txtNeeded);
	    text.setText(Util.alignText(d.Needed));
	    
	    text = (TextView) convertView.findViewById(R.id.txtActual);
	    text.setText(Util.alignText(d.Actual)); 
	    
	    convertView.setOnClickListener(new OnClickListener() {
	      @Override
	      public void onClick(View v) {
	        Toast.makeText(activity, d.department_name, Toast.LENGTH_SHORT).show();
	      }
	    });
	    return convertView;
	  }

	  @Override
	  public int getChildrenCount(int groupPosition) {
	    return groups.get(groupPosition).details.size();
	  }

	  @Override
	  public Object getGroup(int groupPosition) {
	    return groups.get(groupPosition);
	  }

	  @Override
	  public int getGroupCount() {
	    return groups.size();
	  }

	  @Override
	  public void onGroupCollapsed(int groupPosition) {  
	    super.onGroupCollapsed(groupPosition);
	  }

	  @Override
	  public void onGroupExpanded(int groupPosition) {
  	    super.onGroupExpanded(groupPosition);
	  }
  	 

	  @Override
	  public long getGroupId(int groupPosition) {
	    return 0;
	  }

	  @Override
	  public View getGroupView(int groupPosition, boolean isExpanded,
	      View convertView, ViewGroup parent) {
		  
		TextView text = null;
	    if (convertView == null) {
	      convertView = inflater.inflate(R.layout.listrow_groups, null);
	    }
	    Group group = (Group) getGroup(groupPosition);
	    
	    text = (TextView) convertView.findViewById(R.id.txtStationeryName);
	    text.setText(group.master.stationery_name);
	    
	    text = (TextView) convertView.findViewById(R.id.txtNeeded);
	    text.setText(String.valueOf(group.master.Needed));
	    
	    text = (TextView) convertView.findViewById(R.id.txtRetrieved);
	    text.setText(String.valueOf(group.master.Retrieved));
	    
	    /*((CheckedTextView) convertView).setText(group.master.stationery_name);
	    ((CheckedTextView) convertView).setChecked(isExpanded); */
	    
	    ImageView image = (ImageView) convertView.findViewById(R.id.imageView1);
	    
	    if (isExpanded) {
	    	image.setImageResource(android.R.drawable.ic_menu_close_clear_cancel);
	    } else {
	    	image.setImageResource(android.R.drawable.ic_menu_add);
	    }
	    
	    
	    return convertView;
	  }

	  @Override
	  public boolean hasStableIds() {
	    return false;
	  }

	  @Override
	  public boolean isChildSelectable(int groupPosition, int childPosition) {
	    return false;
	  }
}
