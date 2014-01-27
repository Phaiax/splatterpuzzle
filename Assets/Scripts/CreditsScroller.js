#pragma strict

var speed = 0.2;
var crawling = false;

function Start()
{
	// init text here, more space to work than in the Inspector (but you could do that instead)
	var tc = GetComponent(GUIText);
	var creds = "                                      Credits\n\n";
	creds += "„The Talking Dead – the real splatter puzzle for reals“\n";
	creds += "   Developed by\n   Heinekken Planet\n\n";
	
	creds += "Game Directors\n";
	creds += "   Hannah Koch\n   Jan the Horrible\n   Mireille Greene\n   Phaiax\n   Turon the Great\n\n";
	
	creds += "Executive Producers\n";
	creds += "   Hannah Koch\n   Jan the Horrible\n   Mireille Greene\n   Phaiax\n   Turon the Great\n\n";
	
	creds += "Technical Directors\n";
	creds += "   Jan the Horrible\n   Phaiax\n   Turon the Great\n\n";
	
	creds += "Art Director\n";
	creds += "   Hannah Koch\n\n";
	
	creds += "Lead Audio Programmer\n";
	creds += "   Mireille Greene\n\n";
	
	creds += "Audio Director\n";
	creds += "   Mireille Greene\n\n";
	
	creds += "Lead Designer\n";
	creds += "   Hannah Koch\n\n";
	
	creds += "Animators\n";
	creds += "   Jan the Horrible\n   Phaiax\n   Turon the Great\n\n";
	
	creds += "Lead Narrative Designer\n";
	creds += "   Mireille Greene\n\n";
	
	creds += "Lead Level Designer\n";
	creds += "   Hannah Koch\n\n";
	
	creds += "Lead Character Artist\n";
	creds += "   Mireille Greene\n\n";
	
	creds += "Technical Artists\n";
	creds += "   Jan the Horrible\n   Phaiax\n   Turon the Great\n\n";
	
	creds += "Lead Special Effect Artist\n";
	creds += "   Jan the Horrible\n   Phaiax\n   Turon the Great\n\n";
	
	creds += "Sound Designer\n";
	creds += "   Mireille Greene\n\n";
	
	creds += "Concept Artist\n";
	creds += "   Hannah Koch\n   Jan the Horrible\n   Mireille Greene\n   Phaiax\n   Turon the Great\n\n";
	
	creds += "Lead Narrative Designer\n";
	creds += "   Mireille Greene\n   Hannah Koch\n\n";
	
	creds += "AI Programmer\n";
	creds += "   Jan the Horrible\n   Phaiax\n   Turon the Great\n\n";
	
	creds += "Lead Player Programmer\n";
	creds += "   Jan the Horrible\n   Phaiax\n   Turon the Great\n\n";
	
	creds += "Engine Programmer\n";
	creds += "   Unity\n\n";
	
	creds += "Office Manager\n";
	creds += "   Jesus\n\n";
	
	creds += "Lead Q&A\n";
	creds += "   Hannah Koch\n   Jan the Horrible\n   Mireille Greene\n   Phaiax\n   Turon the Great\n\n";
	
	creds += "Catering\n";
	creds += "   Gamelab Karlsruhe\n\n";
	
	creds += "Best Boy\n";
	creds += "   Lionel Ritchie\n\n";
	
	creds += "Emotional Support\n";
	creds += "   Unsere Laptops\n   The Gamelab\n   Mom & Dad\n   David Foster Wallace\n   King Kong\n   Marina Diamandis\n   Chuck Norris\n   Captain Picard\n\n";
	
	creds += "Special Thanks to\n";
	creds += "   The awesome musician Mark Johnston\n   The Game Jam Community\n   Martin Buntz\n   Mixxed up Energy Drinks\n\n";
	
	creds += "\n\n                     Thanks for playing!\n\n";
	
	creds += "                     Press O to restart the fun!";
	tc.text = creds;
	
}

function Update ()
{
	if (!crawling)
		return;
	transform.Translate(Vector3.up * Time.deltaTime * speed);
	if (gameObject.transform.position.y > 13)
	{
		crawling = false;
	}
}