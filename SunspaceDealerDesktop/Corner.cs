﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SunspaceDealerDesktop
{
    public class Corner : LinearItem
    {
        private int itemIndex; //LinearItems Array Index
        private bool angleIs90; //True if 90, False if 45
        private String colour; //Colour of the corner
        private bool outsideCorner; //True is Normal Corner, False if inside corner

        public Corner() { }

        public int ItemIndex
        {
            get
            {
                return itemIndex;
            }

            set
            {
                itemIndex = value;
            }
        }

        public bool AngleIs90
        {
            get
            {
                return angleIs90;
            }

            set
            {
                angleIs90 = value;
            }
        }

        public String Colour
        {
            get
            {
                return colour;
            }

            set
            {
                colour = value.ToLower();
            }
        }

        public bool OutsideCorner
        {
            get
            {
                return outsideCorner;
            }

            set
            {
                outsideCorner = value;
            }
        }
    }
}