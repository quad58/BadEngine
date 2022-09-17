﻿public class MonoBehaviour
{
    public MonoBehaviour()
    {
        InitializeScript();
    }

    public void InitializeScript()
    {
        BadEngine.Program.Start += Start;
        BadEngine.Program.GLStart += GLStart;
        BadEngine.Program.Update += Update;
        BadEngine.Program.Stop += Stop;
    }

    public virtual void Start()
    {
            
    }

    public virtual void GLStart()
    {
            
    }

    public virtual void Update()
    {
            
    }

    public virtual void Stop()
    {
            
    }
}