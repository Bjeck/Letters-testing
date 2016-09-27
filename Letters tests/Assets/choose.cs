﻿/*
------------------------------------------------
Generated by Cradle 2.0.0.0 on 09/26/2016 12:25:42
https://github.com/daterre/Cradle

Original file: choose.html
Story format: Harlowe
------------------------------------------------
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradle;
using IStoryThread = System.Collections.Generic.IEnumerable<Cradle.StoryOutput>;
using Cradle.StoryFormats.Harlowe;

public partial class @choose: Cradle.StoryFormats.Harlowe.HarloweStory
{
	#region Variables
	// ---------------

	public class VarDefs: RuntimeVars
	{
		public VarDefs()
		{
		}

	}

	public new VarDefs Vars
	{
		get { return (VarDefs) base.Vars; }
	}

	// ---------------
	#endregion

	#region Initialization
	// ---------------

	public readonly Cradle.StoryFormats.Harlowe.HarloweRuntimeMacros macros1;

	@choose()
	{
		this.StartPassage = "Intro";

		base.Vars = new VarDefs() { Story = this, StrictMode = true };

		macros1 = new Cradle.StoryFormats.Harlowe.HarloweRuntimeMacros() { Story = this };
	}
	
	void Awake() {
		base.Init();
		passage1_Init();
		passage2_Init();
		passage3_Init();
		passage4_Init();
		passage5_Init();
		passage6_Init();
		passage7_Init();
		passage8_Init();
		passage9_Init();
	}

	// ---------------
	#endregion

	// .............
	// #1: Intro

	void passage1_Init()
	{
		this.Passages[@"Intro"] = new StoryPassage(@"Intro", new string[]{  }, passage1_Main);
	}

	#line 1 "Intro"
	IStoryThread passage1_Main()
	{
		yield return text("It’s simple. I face three options on a daily basis. A loop of my own creation, an attempt to find the world through what I can only assume to be what isn’t here anymore, but instead a little closer to the world as I saw it yesterday. Perhaps that’s the measly point.");
		yield return lineBreak();
		yield return text("What could be better?");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("Option 1", "Option 1", null);
		yield return lineBreak();
		yield return link("Option 2", "Option 2", null);
		yield return lineBreak();
		yield return link("Option 3", "Option 3", null);
		yield break;
	}


	// .............
	// #2: Option 1

	void passage2_Init()
	{
		this.Passages[@"Option 1"] = new StoryPassage(@"Option 1", new string[]{  }, passage2_Main);
	}

	#line 1 "Option 1"
	IStoryThread passage2_Main()
	{
		yield return text("I go forward. Backwards isn’t an option. I can only go where I can see, strafe sideways and claw my way through the milky terrain and spatter along the silky railways, the dark, distant claws that end us all in a hopeful, desperate attempt to find the world in a slightly better place than where we are now. What could be better?");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("Option 2", "Option 2", null);
		yield return lineBreak();
		yield return link("Option 3", "Option 3", null);
		yield return lineBreak();
		yield return link("Option 4", "Option 4", null);
		yield break;
	}


	// .............
	// #3: Option 2

	void passage3_Init()
	{
		this.Passages[@"Option 2"] = new StoryPassage(@"Option 2", new string[]{  }, passage3_Main);
	}

	#line 1 "Option 2"
	IStoryThread passage3_Main()
	{
		yield return text("I don’t move at all. It isn’t a requirement. No one told me to. I realize this and still I find it difficult to tread water, to flail about in a wallowed, hollow mess of a spot, without a channel for recourse and without a strike at the world’s most hideous clasping dooms. No, instead, we find it to be very classically judgded, just unacceptable, avoidable, to stand still. And so we seek other options. What could be better?");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("Option 1", "Option 1", null);
		yield return lineBreak();
		yield return link("Option 3", "Option 3", null);
		yield return lineBreak();
		yield return link("Option 4", "Option 4", null);
		yield break;
	}


	// .............
	// #4: Option 3

	void passage4_Init()
	{
		this.Passages[@"Option 3"] = new StoryPassage(@"Option 3", new string[]{  }, passage4_Main);
	}

	#line 1 "Option 3"
	IStoryThread passage4_Main()
	{
		yield return text("I ask for help. It seems obvious in retrospect. Always in retrospect. Always looking behind. Always knowing where we’ve gone and who took us there, but never, no never, seeing the hopeless thunders that passed us in the night. Is it even time for asking? Should there not be a hope that we can once understand what we need to by our own design, when nothing comes to us in a free-form, desperate manner that finally speculates ourselves to a dream-state and a world without tears? What could be better?");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("Option 1", "Option 1", null);
		yield return lineBreak();
		yield return link("Option 2", "Option 2", null);
		yield return lineBreak();
		yield return link("Option 4", "Option 4", null);
		yield break;
	}


	// .............
	// #5: Option 4

	void passage5_Init()
	{
		this.Passages[@"Option 4"] = new StoryPassage(@"Option 4", new string[]{  }, passage5_Main);
	}

	#line 1 "Option 4"
	IStoryThread passage5_Main()
	{
		yield return text("No. I said three options. There are no more to pick from. The lack of availability grants us strength in the miniscule, tiny ways we find comforting, the different attempts and clouding our judgment through a veil of thin metal that shadows us, becomes us, becomes real and final with a certification that we did not agree to. The finality of knowing your limits, the expression of boundaries, that’s the true freedom, the true power of choice. What could be better?");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("Option 5", "Option 5", null);
		yield break;
	}


	// .............
	// #6: Option 5

	void passage6_Init()
	{
		this.Passages[@"Option 5"] = new StoryPassage(@"Option 5", new string[]{  }, passage6_Main);
	}

	#line 1 "Option 5"
	IStoryThread passage6_Main()
	{
		yield return text("Now it’s just getting silly. Why wouldn’t I stop? Why wouldn’t the world already end at this point, making the entire thing tick and collapse under it’s own weight in a hopeless, desperate fight to survive in a time where options ARE limited and not just something that can be pulled out of a hat in times of need. No, we know what we’re dealing with. That is our strength. That is our frivolity and our great gift. What could be better?");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("Option 6", "Option 6", null);
		yield break;
	}


	// .............
	// #7: Option 6

	void passage7_Init()
	{
		this.Passages[@"Option 6"] = new StoryPassage(@"Option 6", new string[]{  }, passage7_Main);
	}

	#line 1 "Option 6"
	IStoryThread passage7_Main()
	{
		yield return text("Oh please no. Don’t even consider it. Just rest in peace and lie down, forget this ever happened and think back to a time when it all made more sense, when the world was finite and controlled, where even the best of options were only a relation between considerations, not a mass of endless frictions that dance through each other like hair without a head. These dark times, when it is all forgotten and our hopes are not what they once were, these are the times we should forget. We should have never gotten this far. We should not a grasped what we could not see. What could be better?");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("Option 7", "Option 7", null);
		yield break;
	}


	// .............
	// #8: Option 7

	void passage8_Init()
	{
		this.Passages[@"Option 7"] = new StoryPassage(@"Option 7", new string[]{  }, passage8_Main);
	}

	#line 1 "Option 7"
	IStoryThread passage8_Main()
	{
		yield return text("Hope is beyond us. Life is beyond us. We have lost our ability to not only choose, but to dream, to scale ourselves wildly, to make bold decisions based on fact and reason. We are no longer here because we tell ourselves it is our only choice. We are here because we discarded all the other options. Couldn’t we attempt to find a path that made sense? Instead of this endless searching? Wouldn’t it be easier to stick with the first option instead of asking, crying, where we’re going? Instead of asking what could be better?");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("Option 8", "Option 8", null);
		yield break;
	}


	// .............
	// #9: Option 8

	void passage9_Init()
	{
		this.Passages[@"Option 8"] = new StoryPassage(@"Option 8", new string[]{  }, passage9_Main);
	}

	#line 1 "Option 8"
	IStoryThread passage9_Main()
	{
		yield return text("Yes. Here it is. We found eternal chaos. Eight. It is right here, in front of us. We can barely contain it’s power and we have nowhere to go. Please help us. Send help, for we are lost in the vast expanse of what we cannot even consider. Do you remember our first options? Do you remember the choices we didn’t make? Can you help us find back? Can you send help? Could you?");
		yield return lineBreak();
		yield return text("What could be better?");
		yield return lineBreak();
		yield return lineBreak();
		yield return lineBreak();
		yield return link("Option 0", "Intro", null);
		yield break;
	}


}