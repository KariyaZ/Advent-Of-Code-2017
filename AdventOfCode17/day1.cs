﻿namespace AdventOfCode17
{
    using System;
    public class day1
    {
        public day1()
        {
            string puzzleinput = "29917128875332952564321392569634257121244516819997569284938677239676779378822158323549832814412597817651244117851771257438674567254146559419528411463781241159837576747416543451994579655175322397355255587935456185669334559882554936642122347526466965746273596321419312386992922582836979771421518356285534285825212798113159911272923448284681544657616654285632235958355867722479252256292311384799669645293812691169936746744856227797779513997329663235176153745581296191298956836998758194274865327383988992499115472925731787228592624911829221985925935268785757854569131538763133427434848767475989173579655375125972435359317237712667658828722623837448758528395981635746922144957695238318954845799697142491972626942976788997427135797297649149849739186827185775786254552866371729489943881272817466129271912247236569141713377483469323737384967871876982476485658337183881519295728697121462266226452265259877781881868585356333494916519693683238733823362353424927852348119426673294798416314637799636344448941782774113142925315947664869341363354235389597893211532745789957591898692253157726576488811769461354938575527273474399545366389515353657644736458182565245181653996192644851687269744491856672563885457872883368415631469696994757636288575816146927747179133188841148212825453859269643736199836818121559198563122442483528316837885842696283932779475955796132242682934853291737434482287486978566652161245555856779844813283979453489221189332412315117573259531352875384444264457373153263878999332444178577127433891164266387721116357278222665798584824336957648454426665495982221179382794158366894875864761266695773155813823291684611617853255857774422185987921219618596814446229556938354417164971795294741898631698578989231245376826359179266783767935932788845143542293569863998773276365886375624694329228686284863341465994571635379257258559894197638117333711626435669415976255967412994139131385751822134927578932521461677534945328228131973291962134523589491173343648964449149716696761218423314765168285342711137126239639867897341514131244859826663281981251614843274762372382114258543828157464392";
            //int sum = 0;
            int sum = day1solve2(puzzleinput);
            Console.WriteLine(sum);
        }
        //Sum of all digits that match the next digit in the list
        public int day1solve(string puzzleinput)
        {
            int sum = 0;
            for (int i = 0; i < puzzleinput.Length; i++)
            {
                if (i.Equals(puzzleinput.Length-1))
                {
                    if (puzzleinput[i].Equals(puzzleinput[0]))
                    {
                        sum += puzzleinput[i] - '0';
                    }
                } else
                {
                    if (puzzleinput[i].Equals(puzzleinput[i + 1]))
                    {
                        sum += puzzleinput[i] - '0';
                    }
                }
            }
            return sum;
        }

        //Matching halfway around
        public int day1solve2(string puzzleinput)
        {
            int sum = 0;
            for (int i = 0; i < puzzleinput.Length/2; i++)
            {
                if (i.Equals(puzzleinput.Length/2 - 1))
                {
                    if (puzzleinput[i].Equals(puzzleinput[puzzleinput.Length-1]))
                    {
                        sum += (puzzleinput[i] - '0')*2;
                    }
                }
                else
                {
                    if (puzzleinput[i].Equals(puzzleinput[i + puzzleinput.Length / 2]))
                    {
                        sum += (puzzleinput[i] - '0')*2;
                    }
                }
            }
            return sum;
        }
    }

}


