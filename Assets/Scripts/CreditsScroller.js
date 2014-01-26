#pragma strict

var speed = 0.2;
var crawling = false;

function Start()
{
	// init text here, more space to work than in the Inspector (but you could do that instead)
	var tc = GetComponent(GUIText);
	var creds = "Executive Producer:\nMr. Moneybags\n";
	creds += "Art Director:\nArt Guy\n";
	creds += "Technical Director:\nJohn Yaya\n";
	creds += "Programming:\nPoindexter Kopnik\n";
	creds += "Level Design:\nRandy Bugger\n";
	tc.text = creds;
}

function Update ()
{
	if (!crawling)
		return;
	transform.Translate(Vector3.up * Time.deltaTime * speed);
	if (gameObject.transform.position.y > 100)
	{
		crawling = false;
	}
}