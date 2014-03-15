package entity;

import java.util.HashMap;

public class OAuthViewModel extends HashMap<String, String>{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	public String accessToken;
	
	public OAuthViewModel(String accessToken)
	{
		this.accessToken=accessToken;
		put("accessToken", accessToken);
	}
}
