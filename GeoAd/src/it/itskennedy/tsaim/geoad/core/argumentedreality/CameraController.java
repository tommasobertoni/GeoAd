package it.itskennedy.tsaim.geoad.core.argumentedreality;

import java.io.IOException;
import java.util.List;

import android.content.Context;
import android.hardware.Camera;
import android.hardware.Camera.Parameters;
import android.view.SurfaceHolder;
import android.view.SurfaceView;

@SuppressWarnings("deprecation")
public class CameraController extends SurfaceView implements SurfaceHolder.Callback 
{
	private Camera mCamera;
	private SurfaceHolder mHolder;
	
	public CameraController(Context context) 
	{
		super(context, null);
		mCamera = Camera.open();
		mHolder = getHolder();
	    mHolder.addCallback(this);
	}

	public void surfaceCreated(SurfaceHolder holder)
	{
		try 
		{
			mCamera.setPreviewDisplay(holder);
		}
		catch (IOException e)
		{
			
		}
	}

	public void surfaceChanged(SurfaceHolder arg0, int arg1, int arg2, int arg3)
	{
	    mCamera.startPreview();
	}
	   
	public void surfaceDestroyed(SurfaceHolder arg0)
	{
		if (mCamera != null)
		{
	        mCamera.stopPreview();
	        mCamera.release();
	    }
	}
	
}