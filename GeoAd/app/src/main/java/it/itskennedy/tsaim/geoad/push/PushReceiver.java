package it.itskennedy.tsaim.geoad.push;


import android.app.Activity;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.content.WakefulBroadcastReceiver;
import android.util.Log;

import it.itskennedy.tsaim.geoad.core.Engine;
import it.itskennedy.tsaim.geoad.services.GeoAdService;

public class PushReceiver extends WakefulBroadcastReceiver 
{
    @Override
    public void onReceive(Context context, Intent intent) 
    {
        Log.d(Engine.APP_NAME, "Push received");

        Bundle vExtras = intent.getExtras();

        if(!vExtras.isEmpty())
        {
            String vJsonOffer = vExtras.getString("json_obj");

            Intent vDispatchOffer = new Intent();
            vDispatchOffer.setAction(GeoAdService.OFFER_ACTION);
            vDispatchOffer.putExtra(GeoAdService.OFFER_DATA, vJsonOffer);
            context.sendBroadcast(vDispatchOffer);
        }
    }
}