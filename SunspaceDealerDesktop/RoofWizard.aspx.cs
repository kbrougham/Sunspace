﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SunspaceDealerDesktop
{
    public partial class RoofWizard : System.Web.UI.Page
    {
        public float FOAM_PANEL_PROJECTION = Constants.FOAM_PANEL_PROJECTION;
        public float ACRYLIC_PANEL_PROJECTION = Constants.ACRYLIC_PANEL_PROJECTION;
        public float THERMADECK_PANEL_PROJECTION = Constants.THERMADECK_PANEL_PROJECTION;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Temporary session declarations
                string[] tempArray = new String[27];
                tempArray[26] = "Studio";
                tempArray[26] = "Dealer Gable";
                Session.Add("newProjectArray", tempArray);
                Session.Add("sunroomProjection", 101.86576858656);
                Session.Add("sunroomWidth", 100);
                Session.Add("roofSlope", 1.2);
                Session.Add("soffitLength", 0);
                Session.Add("isStandalone", "false");

                //Create a temporary fake list of walls, will use a [ shape, W/S/E walls going from 120 backwall to 110 front wall, 120 projection, 120 width, to match other fake variables
                List<Wall> aListOfWalls = new List<Wall>();

                //Wall aWall = new Wall();
                //aWall.Length = 100;
                //aWall.StartHeight = 120;
                //aWall.EndHeight = 110;
                //aWall.Orientation = "W";

                //aListOfWalls.Add(aWall);

                //aWall = new Wall();
                //aWall.Length = 100;
                //aWall.StartHeight = 110;
                //aWall.EndHeight = 110;
                //aWall.Orientation = "S";

                //aListOfWalls.Add(aWall);

                //aWall = new Wall();
                //aWall.Length = 100;
                //aWall.StartHeight = 110;
                //aWall.EndHeight = 120;
                //aWall.Orientation = "E";

                //aListOfWalls.Add(aWall);

                Wall aWall = new Wall();
                aWall.Length = 100;
                aWall.StartHeight = 100;
                aWall.EndHeight = 100;
                aWall.Orientation = "W";

                aListOfWalls.Add(aWall);

                aWall = new Wall();
                aWall.Length = 100;
                aWall.StartHeight = 100;
                aWall.EndHeight = 100;
                aWall.Orientation = "S";

                aListOfWalls.Add(aWall);

                aWall = new Wall();
                aWall.Length = 100;
                aWall.StartHeight = 100;
                aWall.EndHeight = 100;
                aWall.Orientation = "E";

                aListOfWalls.Add(aWall);

                Session.Add("listOfWalls", aListOfWalls);
                //slope
                //enter an overhang #
                //include gutter in overhang

                #region Dropdown Population
                //Thickness
                for (int i = 0; i < Constants.ROOF_TRADITIONAL_THICKNESSES.Length; i++)
                {
                    ddlThickness.Items.Add(new ListItem(Constants.ROOF_TRADITIONAL_THICKNESSES[i], Constants.ROOF_TRADITIONAL_THICKNESSES[i]));
                }
                //Thermadeck Thickness
                for (int i = 0; i < Constants.ROOF_THERMADECK_THICKNESSES.Length; i++)
                {
                    ddlThermadeckThickness.Items.Add(new ListItem(Constants.ROOF_THERMADECK_THICKNESSES[i], Constants.ROOF_THERMADECK_THICKNESSES[i]));
                }
                //Acrylic Thickness
                for (int i = 0; i < Constants.ROOF_ACRYLIC_THICKNESSES.Length; i++)
                {
                    ddlAcrylicThickness.Items.Add(new ListItem(Constants.ROOF_ACRYLIC_THICKNESSES[i], Constants.ROOF_ACRYLIC_THICKNESSES[i]));
                }
                //Acrylic Colour
                for (int i = 0; i < Constants.ACRYLIC_COLOUR.Length; i++)
                {
                    ddlAcrylicColour.Items.Add(new ListItem(Constants.ACRYLIC_COLOUR[i], Constants.ACRYLIC_COLOUR[i]));
                }
                //Extrusion Type
                for (int i = 0; i < Constants.ROOF_EXTRUSION_TYPE.Length; i++)
                {
                    ddlPanelType.Items.Add(new ListItem(Constants.ROOF_EXTRUSION_TYPE[i], Constants.ROOF_EXTRUSION_TYPE[i]));
                }
                //Interior Skin
                for (int i = 0; i < Constants.ROOF_INTERIOR_SKIN_TYPES.Length; i++)
                {
                    ddlInteriorRoofSkin.Items.Add(new ListItem(Constants.ROOF_INTERIOR_SKIN_TYPES[i], Constants.ROOF_INTERIOR_SKIN_TYPES[i]));
                }
                //Exterior Skin
                for (int i = 0; i < Constants.ROOF_EXTERIOR_SKIN_TYPES.Length; i++)
                {
                    ddlExteriorRoofSkin.Items.Add(new ListItem(Constants.ROOF_EXTERIOR_SKIN_TYPES[i], Constants.ROOF_EXTERIOR_SKIN_TYPES[i]));
                }
                //gutter/fascia colour
                for (int i = 0; i < Constants.GUTTER_COLOUR.Length; i++)
                {
                    ddlGutterColour.Items.Add(new ListItem(Constants.GUTTER_COLOUR[i], Constants.GUTTER_COLOUR[i]));
                }
                //extra downspouts
                for (int i = 1; i <= 10; i++)
                {
                    ddlExtraDownspouts.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                //stripe colour
                for (int i = 0; i < Constants.FASCIA_STRIPE_COLOUR.Length; i++)
                {
                    ddlStripeColour.Items.Add(new ListItem(Constants.FASCIA_STRIPE_COLOUR[i], Constants.FASCIA_STRIPE_COLOUR[i]));
                }
                #endregion
            }

            //Check roof type, position 26
            string[] gableCheck = (string[])Session["newProjectArray"];
            if (gableCheck[26] == "Dealer Gable" || gableCheck[26] == "Sunspace Gable")
            {
                FOAM_PANEL_PROJECTION += FOAM_PANEL_PROJECTION;
                ACRYLIC_PANEL_PROJECTION += ACRYLIC_PANEL_PROJECTION;
                THERMADECK_PANEL_PROJECTION += THERMADECK_PANEL_PROJECTION;
            }
        }

        protected void btnFinalButton_Click(object sender, EventArgs e)
        {
            //Check roof type, position 26
            string[] newProjectArray = (string[])Session["newProjectArray"];

            //Get room projection and width from session and set our roofs projection and width to those
            float roofProjection = Convert.ToSingle(Session["sunroomProjection"]);
            float roofWidth = Convert.ToSingle(Session["sunroomWidth"]);

            //We also get slope of room, and soffit length
            float roofSlope = Convert.ToSingle(Session["roofSlope"]);
            float soffitLength = Convert.ToSingle(Session["soffitLength"]);            

            #region Gable System
            //if gable, we need two studio roof systems and additional logic
            if (newProjectArray[26] == "Dealer Gable" || newProjectArray[26] == "Sunspace Gable")
            {            
                //If they've entered manual dimensions, we don't need to calculate overhang
                if (hidProjection.Value != "")
                {
                    //However, if its smaller than the room, we need to throw an error
                    if (Convert.ToSingle(hidProjection.Value) >= roofProjection)
                    {
                        //Since its valid, just set our sizes to the specified
                        roofProjection = Convert.ToSingle(hidProjection.Value);
                        roofWidth = Convert.ToSingle(hidWidth.Value);
                    }
                }    
                else
                {
                    roofProjection += (Convert.ToSingle(hidOverhang.Value) * 2);
                    roofWidth += (Convert.ToSingle(hidOverhang.Value) * 2);
                }

                //Convert the room projection into actual roof projection
                roofProjection = (float)Math.Sqrt(Math.Pow(2*roofProjection, 2));
                roofProjection /= 2; //divide by 2 for each part of a gable

                //Convert to nearest eighth inch
                roofProjection *= 8;
                int intRoofProjection = Convert.ToInt32(roofProjection);
                roofProjection = intRoofProjection / 8f;
                roofProjection *= 2; //remultiply to get whole projection

                //Now that we have roof rojection and width, add it to session.
                Session.Add("roofProjection", (roofProjection));
                Session.Add("roofWidth", roofWidth);
                lblTest.Text = roofProjection.ToString() + " by " + roofWidth.ToString();

                //A studio roof will only have one list entry, while a gable will have two
                List<RoofModule> gableModules = buildGableRoofModule((roofProjection), roofWidth);

                bool isFireProtected = false;
                bool isThermadeck = false;
                bool hasGutters = false;
                bool gutterPro = false;

                if (hidPanelType.Value.Contains("FP"))
                {
                    isFireProtected = true;
                }

                if (hidSystem.Value == "Thermadeck")
                {
                    isThermadeck = true;
                }

                if (hidGutterPresence.Value == "Yes")
                {
                    hasGutters = true;
                }

                if (hidGutterPro.Value == "Yes")
                {
                    gutterPro = true;
                }

                //changeme hardcoded supports to 0
                Roof aRoof = new Roof("Dealer Gable", hidInteriorRoofSkin.Value, hidExteriorRoofSkin.Value, Convert.ToSingle(hidThickness.Value), isFireProtected, isThermadeck, hasGutters, gutterPro, hidGutterColour.Value, hidStripeColour.Value, 0, roofProjection, roofWidth, gableModules);
                Session.Add("completedRoof", aRoof);

                Response.Redirect("RoofTesting.aspx");
            }
            #endregion

            #region Studio System
            //studio system
            else
            {
                //Subtract soffit length as that will be the true start point of the roof
                roofProjection -= Convert.ToSingle(Session["soffitLength"]);

                //If they've entered manual dimensions, we don't need to calculate overhang
                if (hidProjection.Value != "")
                {
                    //However, if its smaller than the room, we need to throw an error
                    if (Convert.ToSingle(hidProjection.Value) >= roofProjection)
                    {
                        //Since its valid, just set our sizes to the specified
                        roofProjection = Convert.ToSingle(hidProjection.Value);
                        roofWidth = Convert.ToSingle(hidWidth.Value);
                    }
                }
                else
                {
                    roofProjection += (Convert.ToSingle(hidOverhang.Value) * 2);
                    roofWidth += (Convert.ToSingle(hidOverhang.Value) * 2);
                }
            
                //Convert the room projection into actual roof projection
                float actualSlope = (Convert.ToSingle(Session["roofSlope"])) / 12;
                float roofRise = actualSlope * roofProjection;

                roofProjection = (float)Math.Sqrt(Math.Pow(roofRise,2) + Math.Pow(roofProjection, 2));

                //Convert to nearest eighth inch
                roofProjection *= 8;
                int intRoofProjection = Convert.ToInt32(roofProjection);
                roofProjection = intRoofProjection/8f;

                //Now that we have roof rojection and width, add it to session.
                Session.Add("roofProjection", roofProjection);
                Session.Add("roofWidth", roofWidth);
                lblTest.Text = roofProjection.ToString() + " by " + roofWidth.ToString();

                //A studio roof will only have one list entry, while a gable will have two
                List<RoofModule> aModuleList = new List<RoofModule>();
                aModuleList.Add(buildStudioRoofModule(roofProjection, roofWidth));

                bool isFireProtected = false;
                bool isThermadeck = false;
                bool hasGutters = false;
                bool gutterPro = false;

                if (hidPanelType.Value.Contains("FP"))
                {
                    isFireProtected = true;
                }

                if (hidSystem.Value == "Thermadeck")
                {
                    isThermadeck = true;
                }

                if (hidGutterPresence.Value == "Yes")
                {
                    hasGutters = true;
                }

                if (hidGutterPro.Value == "Yes")
                {
                    gutterPro = true;
                }

                //changeme hardcoded supports to 0
                Roof aRoof = new Roof("Studio", hidInteriorRoofSkin.Value, hidExteriorRoofSkin.Value, Convert.ToSingle(hidThickness.Value), isFireProtected, isThermadeck, hasGutters, gutterPro, hidGutterColour.Value, hidStripeColour.Value, 0, roofProjection, roofWidth, aModuleList);
                Session.Add("completedRoof", aRoof);

                Response.Redirect("RoofTesting.aspx");
            }
            #endregion
        }

        protected RoofModule buildStudioRoofModule(float roofProjection, float roofWidth)
        {
            //Variables that will be used to build the roof
            float panelWidth;
            string panelType;
            string panelBeamType;
            float panelBeamWidth;

            //set PanelBeamType based on the ddlPanelType value: ie I-beam or pressure cap
            if (hidPanelType.Value.Contains("I-Beam"))
            {
                panelBeamType = "I-Beam";
                panelBeamWidth = Constants.ROOF_IBEAM_WIDTH;
            }
            else if (hidPanelType.Value.Contains("Pressure Cap"))
            {
                panelBeamType = "Pressure Cap";
                panelBeamWidth = Constants.ROOF_PRESSURECAP_WIDTH;
            }
            else
            {
                panelBeamType = "Thermadeck";
                //Thermadeck uses wood underneath the panels, so there is essentially no width to seperator beams
                panelBeamWidth = 0f;
            }

            //If its an acrylic roof, our panels will use the acrylic constants, otherwise foam
            if (hidSystem.Value == "Traditional")
            {
                panelWidth = Constants.FOAM_PANEL_WIDTH;
                panelType = "Foam Panel";
            }
            else if (hidSystem.Value == "Acrylic")
            {
                panelBeamType = "T-Bar";
                panelWidth = Constants.ACRYLIC_PANEL_WIDTH;
                panelType = "Acrylic Panel";
            }
            else
            {
                panelBeamType = "None";
                panelWidth = Constants.THERMADECK_PANEL_WIDTH;
                panelType = "Thermadeck Panel";
            }

            //build roof objects
            float numberOfPanels = (float)Math.Ceiling(roofWidth / panelWidth); //If it requires 'part' of a panel, that is essentially another panel, just cut. Cut will be handled later.

            //lets start making a list of roof items
            List<RoofItem> itemList = new List<RoofItem>();

            if (hidSystem.Value != "Thermadeck")
            {
                //Add the first panel, because if we loop adding panel+seperator, we will end with one extra
                itemList.Add(new RoofItem(panelType, roofProjection, panelWidth));

                //loop adding seperator then panels, minus one iteration because one panel is already added
                for (int i = 0; i < (numberOfPanels - 1); i++)
                {
                    itemList.Add(new RoofItem(panelBeamType, roofProjection, (float)panelBeamWidth));
                    itemList.Add(new RoofItem(panelType, roofProjection, panelWidth));
                }
            }
            //if it is thermadeck
            else
            {
                for (int i = 0; i < numberOfPanels; i++)
                {
                    itemList.Add(new RoofItem(panelType, roofProjection, panelWidth));
                }
            }
            float itemWidthTotal = 0;

            //Total width of items
            for (int i = 0; i < itemList.Count; i++)
            {
                itemWidthTotal += itemList[i].Width;
            }

            //If this width doesn't fit perfectly (is more than roof width) we'll need to make a cut on the last panel
            if (itemWidthTotal > roofWidth)
            {
                //at .count-1 to get last item, which should be the final panel
                //We subtract the difference that the panel exceeds to make the 'cut'
                itemList[itemList.Count - 1].Width -= (itemWidthTotal - roofWidth);
            }

            string panelExteriorSkin = hidExteriorRoofSkin.Value;
            string panelInteriorSkin = hidInteriorRoofSkin.Value;

            if (hidSystem.Value == "Thermadeck")
            {
                //Thermadeck systems must be osb/osb
                panelExteriorSkin = "OSB";
                panelInteriorSkin = "OSB";
            }
            RoofModule aModule = new RoofModule(roofProjection, roofWidth, panelInteriorSkin, panelExteriorSkin, itemList);

            return aModule;
        }

        protected List<RoofModule> buildGableRoofModule(float roofProjection, float roofWidth)
        {
            //Variables that will be used to build the roof
            float panelWidth;
            string panelType;
            string panelBeamType;
            float panelBeamWidth;

            //set PanelBeamType based on the ddlPanelType value: ie I-beam or pressure cap
            if (hidPanelType.Value.Contains("I-Beam"))
            {
                panelBeamType = "I-Beam";
                panelBeamWidth = Constants.ROOF_IBEAM_WIDTH;
            }
            else if (hidPanelType.Value.Contains("Pressure Cap"))
            {
                panelBeamType = "Pressure Cap";
                panelBeamWidth = Constants.ROOF_PRESSURECAP_WIDTH;
            }
            else
            {
                panelBeamType = "Thermadeck";
                //Thermadeck uses wood underneath the panels, so there is essentially no width to seperator beams
                panelBeamWidth = 0f;
            }

            //If its an acrylic roof, our panels will use the acrylic constants, otherwise foam
            if (hidSystem.Value == "Traditional")
            {
                panelWidth = Constants.FOAM_PANEL_WIDTH;
                panelType = "Foam Panel";
            }
            else if (hidSystem.Value == "Acrylic")
            {
                panelBeamType = "T-Bar";
                panelWidth = Constants.ACRYLIC_PANEL_WIDTH;
                panelType = "Acrylic Panel";
            }
            else
            {
                panelBeamType = "None";
                panelWidth = Constants.THERMADECK_PANEL_WIDTH;
                panelType = "Thermadeck Panel";
            }

            //build roof objects
            float numberOfPanels = (float)Math.Ceiling(roofWidth / panelWidth); //If it requires 'part' of a panel, that is essentially another panel, just cut. Cut will be handled later.
                        
            //lets start making a list of roof items
            List<RoofItem> itemList = new List<RoofItem>();
            List<RoofItem> gableList = new List<RoofItem>();

            if (hidSystem.Value != "Thermadeck")
            {
                //Add the first panel, because if we loop adding panel+seperator, we will end with one extra
                //We use roofProjection/2 for the following, because this is just one side of the gable roof, thus half the projection
                itemList.Add(new RoofItem(panelType, roofProjection / 2, panelWidth));

                //loop adding seperator then panels, minus one iteration because one panel is already added
                for (int i = 0; i < (numberOfPanels - 1); i++)
                {
                    itemList.Add(new RoofItem(panelBeamType, roofProjection/2, (float)panelBeamWidth));
                    itemList.Add(new RoofItem(panelType, roofProjection / 2, panelWidth));
                }
            }
            //if it is thermadeck
            else
            {
                for (int i = 0; i < numberOfPanels; i++)
                {
                    itemList.Add(new RoofItem(panelType, roofProjection / 2, panelWidth));
                }
            }
            float itemWidthTotal = 0;

            //Total width of items
            for (int i = 0; i < itemList.Count; i++)
            {
                itemWidthTotal += itemList[i].Width;
            }

            //If this width doesn't fit perfectly (is more than roof width) we'll need to make a cut on the last panel
            if (itemWidthTotal > roofWidth)
            {
                //at .count-1 to get last item, which should be the final panel
                //We subtract the difference that the panel exceeds to make the 'cut'
                itemList[itemList.Count - 1].Width -= (itemWidthTotal - roofWidth);
            }

            string panelExteriorSkin = hidExteriorRoofSkin.Value;
            string panelInteriorSkin = hidInteriorRoofSkin.Value;

            if (hidSystem.Value == "Thermadeck")
            {
                //Thermadeck systems must be osb/osb
                panelExteriorSkin = "OSB";
                panelInteriorSkin = "OSB";
            }

            List<RoofModule> moduleList = new List<RoofModule>();
            RoofModule aModule = new RoofModule(roofProjection / 2, roofWidth, panelInteriorSkin, panelExteriorSkin, itemList);
            moduleList.Add(aModule);

            //We make a second module with the reverse roof items, because the gable is mirrored on the other side
            for (int i = (itemList.Count - 1); i >= 0; i--)
            {
                gableList.Add(itemList[i]);
            }

            RoofModule aSecondModule = new RoofModule(roofProjection / 2, roofWidth, panelInteriorSkin, panelExteriorSkin, gableList);

            moduleList.Add(aSecondModule);

            return moduleList;
        }
    }
}