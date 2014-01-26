var newSkin : GUISkin;
var mapTexture : Texture2D;

function theMapMenu() {
    //layout start
    GUI.BeginGroup(Rect(Screen.width / 2 - 200, 50, 400, 300));
   
    //buttons
    if(GUI.Button(Rect(15, 40, 180, 40), "Level 1")) {
    	Application.LoadLevel(1);
    }
    if(GUI.Button(Rect(15, 100, 180, 40), "Level 2")) {
    	Application.LoadLevel(2);
    }
    
    if(GUI.Button(Rect(205, 250, 180, 40), "go back")) {
    	var script = GetComponent("MainMenuScript");
    	script.enabled = true;
    	var script2 = GetComponent("MapMenuScript");
    	script2.enabled = false;
    }
   
    //layout end
    GUI.EndGroup();
}

function OnGUI () {
    //load GUI skin
    GUI.skin = newSkin;
   
    //execute theMapMenu function
    theMapMenu();
}