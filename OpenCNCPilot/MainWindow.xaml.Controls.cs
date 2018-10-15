using OpenCNCPilot.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Timers;

namespace OpenCNCPilot
{    
    partial class MainWindow
	{
        private System.Timers.Timer mouseDownTimer = null;
        private delegate void JogDelegate();
        JogDelegate jogHandler = null;

        private void ButtonHomeCycle_Click(object sender, RoutedEventArgs e)
		{
            if (machine.Mode != Machine.OperatingMode.Manual)
                return;

            machine.SendLine("$H");
        }

        private string feedrate = "0";       
        
        private void StartTimer()
        {
            if (mouseDownTimer == null)
            {
                mouseDownTimer = new System.Timers.Timer(50);                
                mouseDownTimer.AutoReset = true;
                mouseDownTimer.Elapsed += MouseDownTimer_Elapsed;                
            }

            mouseDownTimer.Start();
        }
        
        private void StopTimer()
        {
            if (mouseDownTimer != null)
            {
                mouseDownTimer.Stop();
            }
        }

        private void MouseDownTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            jogHandler?.Invoke();
        }


        private void HandleFeedrateCheck(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(RadioButton))
            {
                feedrate = ((RadioButton)sender).Content.ToString();
            }
        }       

        private void Controls_JogCanceled()
        {
            ;
        }      

        private void Controls_JogCancel()
        {            
            machine.JogCancel();
        }

        private void Controls_JogXNegative()
        {
            machine.Jog("X", -1.0, int.Parse(feedrate));            
        }

        private void Controls_JogXPositive()
        {
            machine.Jog("X", 1.0, int.Parse(feedrate));
        }


        private void Controls_JogYNegative()
        {
            machine.Jog("Y", -1.0, int.Parse(feedrate));
        }

        private void Controls_JogYPositive()
        {
            machine.Jog("Y", 1.0, int.Parse(feedrate));
        }


        private void Controls_JogZNegative()
        {
            machine.Jog("Z", -1.0, int.Parse(feedrate));
        }

        private void Controls_JogZPositive()
        {
            machine.Jog("Z", 1.0, int.Parse(feedrate));
        }




        private void Controls_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            StopTimer();
            jogHandler = null;
            Controls_JogCancel();
        }

        private void Controls_PreviewMouseDownJogXNegative(object sender, MouseButtonEventArgs e)
        {            
            jogHandler = Controls_JogXNegative;
            StartTimer();
        }

        private void Controls_PreviewMouseDownJogXPositive(object sender, MouseButtonEventArgs e)
        {
            jogHandler = Controls_JogXPositive;
            StartTimer();
        }

        private void Controls_PreviewMouseDownJogYNegative(object sender, MouseButtonEventArgs e)
        {
            jogHandler = Controls_JogYNegative;
            StartTimer();
        }

        private void Controls_PreviewMouseDownJogYPositive(object sender, MouseButtonEventArgs e)
        {
            jogHandler = Controls_JogYPositive;
            StartTimer();
        }

        private void Controls_PreviewMouseDownJogZNegative(object sender, MouseButtonEventArgs e)
        {
            jogHandler = Controls_JogZNegative;
            StartTimer();
        }

        private void Controls_PreviewMouseDownJogZPositive(object sender, MouseButtonEventArgs e)
        {
            jogHandler = Controls_JogZPositive;
            StartTimer();
        }
    }
}
