using System;
using System.Runtime.Remoting.Messaging;


namespace MyGame
{
	public class LoadTimer
	{
	    private System.Timers.Timer aTimer;
	
	    public LoadTimer(SelectionPage s, int interval)
	    {
	        
	        // Consturct timer with 2 second interval check
	        aTimer = new System.Timers.Timer();
	        aTimer.Interval = interval; //e.g. 4 secs = 4000
	
	        // Hook up the Elapsed event for the timer. 
	        //aTimer.Elapsed += OnTimedEvent;
	        aTimer.Elapsed += (sender, e) => OnTimedEvent(sender, e, s);
	
	        // Start the timer
	        aTimer.Enabled = true;
	    }
	
	    private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e, SelectionPage s)
	    {
			
			s.CloseForm();
	    }
	
	}
}