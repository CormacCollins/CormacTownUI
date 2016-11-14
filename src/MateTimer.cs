using System;
using System.Runtime.Remoting.Messaging;

namespace MyGame
{

	public class MateTimer
	{
	    private System.Timers.Timer aTimer;
	
//	    public MateTimer(EntityEnvironment env, int interval)
//	    {
//	        
//	        // Consturct timer with 2 second interval check
//	        aTimer = new System.Timers.Timer();
//	        aTimer.Interval = interval; //e.g. 4 secs = 4000
//	
//	        // Hook up the Elapsed event for the timer. 
//	        //aTimer.Elapsed += OnTimedEvent;
//	        aTimer.Elapsed += (sender, e) => OnTimedEvent(sender, e, env);
//	        // Have the timer fire repeated events (true is the default)
//	        aTimer.AutoReset = true;
//	
//	        // Start the timer
//	        aTimer.Enabled = true;
//	        
//	
//	        // If the timer is declared in a long-running method, use KeepAlive to prevent garbage collection
//	        // from occurring before the method ends. 
//	        GC.KeepAlive(aTimer);
//	    }
//	
//	    private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e, EntityEnvironment env)
//	    {
//	        env.MatingTimeUpdate(new TimeSpan(40000000)); //4 seconds
//	    }
	
	}
}