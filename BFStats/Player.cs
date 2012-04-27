using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BFStats
{
    public class Player
    {
        public string PlayerName;
        public string Rank;
        public int Score;
        public string TimeRaw;
        public int Kills;
        public int Deaths;
        public float Skill;
        public string RankPic;
        public string BackTilePic;
        public int Objective;

        public List<Weapon> Weapons = new List<Weapon>();
        public List<Vehicle> Vehicles = new List<Vehicle>();
        public List<Ribbon> Ribbons = new List<Ribbon>();
        public List<Medal> Medals = new List<Medal>();
        public List<Specialization> Specs = new List<Specialization>();
        public List<VehicleType> VehicleTypes = new List<VehicleType>();
        public List<Equipment> Equipments = new List<Equipment>();

        public List<NextRank> NextRanks = new List<NextRank>();

        public Kit Assault = new Kit();
        public Kit Engineer = new Kit();
        public Kit Recon = new Kit();
        public Kit Support = new Kit();

        public int Wins = 0;
        public int Losses = 0;
        public int Hits = 0;
        public int Shots = 0;

        public string IntDateUpdated;
        public string IntDateChecked; 

        public Player(string json)
        {
            JObject data = JObject.Parse(json);

            this.PlayerName = data["list"][Configs.GetPlayer()]["name"].ToString();
            this.Rank = data["list"][Configs.GetPlayer()]["stats"]["rank"]["nr"].ToString();
            this.Score = Convert.ToInt32(data["list"][Configs.GetPlayer()]["stats"]["scores"]["score"].ToString());
            this.Objective = Convert.ToInt32(data["list"][Configs.GetPlayer()]["stats"]["scores"]["objective"].ToString());
            this.TimeRaw = data["list"][Configs.GetPlayer()]["stats"]["global"]["time"].ToString();
            this.Kills = Convert.ToInt32(data["list"][Configs.GetPlayer()]["stats"]["global"]["kills"].ToString());
            this.Deaths = Convert.ToInt32(data["list"][Configs.GetPlayer()]["stats"]["global"]["deaths"].ToString());
            this.Skill = Convert.ToSingle(data["list"][Configs.GetPlayer()]["stats"]["global"]["elo"].ToString());
            this.Wins = Convert.ToInt32(data["list"][Configs.GetPlayer()]["stats"]["global"]["wins"].ToString());
            this.Losses = Convert.ToInt32(data["list"][Configs.GetPlayer()]["stats"]["global"]["losses"].ToString());
            this.Hits = Convert.ToInt32(data["list"][Configs.GetPlayer()]["stats"]["global"]["hits"].ToString());
            this.Shots = Convert.ToInt32(data["list"][Configs.GetPlayer()]["stats"]["global"]["shots"].ToString());
            this.RankPic = data["list"][Configs.GetPlayer()]["stats"]["rank"]["img_small"].ToString();

            JObject kits = (JObject)data["list"][Configs.GetPlayer()]["stats"]["kits"];

            this.Assault.Score = Convert.ToInt32(kits["assault"]["score"].ToString());
            this.Assault.TimeRaw = kits["assault"]["time"].ToString();
            this.Assault.KitPic = kits["assault"]["img"].ToString();

            this.Engineer.Score = Convert.ToInt32(kits["engineer"]["score"].ToString());
            this.Engineer.TimeRaw = kits["engineer"]["time"].ToString();
            this.Engineer.KitPic = kits["engineer"]["img"].ToString();

            this.Recon.Score = Convert.ToInt32(kits["recon"]["score"].ToString());
            this.Recon.TimeRaw = kits["recon"]["time"].ToString();
            this.Recon.KitPic = kits["recon"]["img"].ToString();

            this.Support.Score = Convert.ToInt32(kits["support"]["score"].ToString());
            this.Support.TimeRaw = kits["support"]["time"].ToString();
            this.Support.KitPic = kits["support"]["img"].ToString();

            JObject weapons = (JObject)data["list"][Configs.GetPlayer()]["stats"]["weapons"];
            var wenum = weapons.GetEnumerator();

            while (wenum.MoveNext())
            {
                Weapon weapon = new Weapon();
                JObject w = JObject.Parse(wenum.Current.Value.ToString());
                weapon.Time = w["time"].ToString();
                weapon.Kills = w["kills"].ToString();
                weapon.Headshots = w["headshots"].ToString();
                weapon.Hits = w["hits"].ToString();
                weapon.Shots = w["shots"].ToString();
                weapon.Stars = w["star"]["count"].ToString();
                weapon.WeaponName = w["name"].ToString();
                weapon.WeaponPic = w["img"].ToString();
                weapon.Current = Convert.ToInt32(w["star"]["curr"].ToString());
                weapon.Needed = Convert.ToInt32(w["star"]["needed"].ToString());
                weapon.Description = w["desc"].ToString();
                weapon.NextStarNeeded = Convert.ToInt32(w["star"]["needed"].ToString());
                weapon.NextStarCurrent = Convert.ToInt32(w["star"]["curr"].ToString());
                weapon.InKit = w["kit"].ToString();
                weapon.Range = w["range"].ToString();
                weapon.FireModeBurst = Convert.ToBoolean(w["fireModeBurst"].ToString());
                weapon.FireModeAuto = Convert.ToBoolean(w["fireModeAuto"].ToString());
                weapon.FireModeSingle = Convert.ToBoolean(w["fireModeSingle"].ToString());
                weapon.ROF = w["rateOfFire"].ToString();
                weapon.Ammo = w["ammo"].ToString();

                JArray unlocks = (JArray)w["unlocks"];
                List<WeaponUnlock> unlocklist = new List<WeaponUnlock>();
                int i = 0;
                while (i <= unlocks.Count-1)
                {
                    JObject u = JObject.Parse(unlocks[i].ToString());
                    if(u["curr"].ToString() != "")
                    {
                        WeaponUnlock wu = new WeaponUnlock();
                        wu.Current = Convert.ToInt32(u["curr"].ToString());
                        wu.Needed = Convert.ToInt32(u["needed"].ToString());
                        wu.UnlockName = u["name"].ToString();
                        wu.UnlockPic = u["img"].ToString();
                        unlocklist.Add(wu);
                    }
                    i++;
                }
                weapon.Unlocks = unlocklist;
                Weapons.Add(weapon);
            }

            Weapons.Sort();

            JObject vehicles = (JObject)data["list"][Configs.GetPlayer()]["stats"]["vehicles"];
            var venum = vehicles.GetEnumerator();

            while (venum.MoveNext())
            {
                Vehicle vehicle = new Vehicle();
                JObject v = JObject.Parse(venum.Current.Value.ToString());
                vehicle.Time = v["time"].ToString();
                vehicle.Kills = v["kills"].ToString();
                vehicle.Destroys = v["destroys"].ToString();
                vehicle.VehicleName = v["name"].ToString();
                vehicle.VehiclePic = v["img"].ToString();
                vehicle.Description = v["desc"].ToString();
                vehicle.TypeKey = v["type"].ToString();

                Vehicles.Add(vehicle);
            }

            Vehicles.Sort();

            JObject ribbons = (JObject)data["list"][Configs.GetPlayer()]["stats"]["ribbons"];
            var renum = ribbons.GetEnumerator();

            while (renum.MoveNext())
            {
                Ribbon ribbon = new Ribbon();
                JObject r = JObject.Parse(renum.Current.Value.ToString());
                ribbon.RibbonName = r["name"].ToString();
                ribbon.RibbonPic = r["img_small"].ToString();
                ribbon.TimesTaken = Convert.ToInt32(r["count"].ToString());
                ribbon.Description = r["desc"].ToString();
                Ribbons.Add(ribbon);
            }

            Ribbons.Sort();

            JObject medals = (JObject)data["list"][Configs.GetPlayer()]["stats"]["medals"];
            var menum = medals.GetEnumerator();

            while (menum.MoveNext())
            {
                Medal medal = new Medal();
                JObject m = JObject.Parse(menum.Current.Value.ToString());
                medal.MedalName = m["name"].ToString();
                medal.MedalPic = m["img_small"].ToString();
                medal.TimesTaken = Convert.ToInt32(m["count"].ToString());
                medal.Description = m["desc"].ToString();
                Medals.Add(medal);
            }

            Medals.Sort();

            JObject specs = (JObject)data["list"][Configs.GetPlayer()]["stats"]["specializations"];
            var senum = specs.GetEnumerator();

            while (senum.MoveNext())
            {
                Specialization spec = new Specialization();
                JObject s = JObject.Parse(senum.Current.Value.ToString());
                spec.SpecName = s["name"].ToString();
                spec.Description = s["desc"].ToString();
                spec.Current = Convert.ToInt32(s["curr"].ToString());
                spec.Needed = Convert.ToInt32(s["needed"].ToString());
                spec.SpecPic = s["img"].ToString();
                Specs.Add(spec);
            }

            JObject vunlocks = (JObject)data["list"][Configs.GetPlayer()]["stats"]["vehcats"];
            var vuenum = vunlocks.GetEnumerator();

            while (vuenum.MoveNext())
            {
                VehicleType vt = new VehicleType();
                JObject v = JObject.Parse(vuenum.Current.Value.ToString());
                vt.TypeName = v["name"].ToString();
                if (vt.TypeName != "Air Jet Attack")
                {
                    vt.TypePic = v["img"].ToString();
                    vt.Kills = Convert.ToInt32(v["kills"].ToString());
                    vt.TimeRaw = v["time"].ToString();
                    vt.Score = Convert.ToInt32(v["score"].ToString());
                    vt.ServiceStars = Convert.ToInt32(v["star"]["count"].ToString());
                    vt.SSCurrent = Convert.ToInt32(v["star"]["curr"].ToString());
                    vt.SSNeeded = Convert.ToInt32(v["star"]["needed"].ToString());

                    JArray unlocks = (JArray)v["unlocks"];
                    List<VehicleUnlock> unlocklist = new List<VehicleUnlock>();
                    int i = 0;
                    while (i <= unlocks.Count - 1)
                    {
                        JObject u = JObject.Parse(unlocks[i].ToString());
                        if (u["curr"].ToString() != "")
                        {
                            VehicleUnlock vu = new VehicleUnlock();
                            vu.Current = Convert.ToInt32(u["curr"].ToString());
                            vu.Needed = Convert.ToInt32(u["needed"].ToString());
                            vu.UnlockName = u["name"].ToString();
                            vu.UnlockPic = u["img"].ToString();
                            vu.Description = u["desc"].ToString();
                            unlocklist.Add(vu);
                        }
                        i++;
                    }
                    vt.Unlocks = unlocklist;
                    VehicleTypes.Add(vt);
                }
            }

            VehicleTypes.Sort();

            Equipment sb = new Equipment();
            sb.EquipName = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqRad"]["name"].ToString();
            sb.Description = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqRad"]["desc"].ToString();
            sb.EquipPic = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqRad"]["img"].ToString();
            sb.KitPic = "misc/Mp_Recon_Sc_Recon.png";
            sb.LineTwo = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqRad"]["spawns"].ToString() + " spawns";
            Equipments.Add(sb);

            Equipment g = new Equipment();
            g.EquipName = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeM67"]["name"].ToString();
            g.Description = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeM67"]["desc"].ToString();
            g.EquipPic = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeM67"]["img"].ToString();
            g.KitPic = "misc/Mp_General_Sc_General.png";
            g.LineTwo = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeM67"]["kills"].ToString() + " kills, " +
                data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeM67"]["hits"].ToString() + " hits in " +
                data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeM67"]["shots"].ToString() + " throws";
            Equipments.Add(g);

            Equipment rt = new Equipment();
            rt.EquipName = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["wasRT"]["name"].ToString();
            rt.Description = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["wasRT"]["desc"].ToString();
            rt.EquipPic = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["wasRT"]["img"].ToString();
            rt.KitPic = "misc/Mp_Engineer_Sc_Engineer.png";
            rt.LineTwo = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["wasRT"]["kills"].ToString() + " kills and " + 
                data["list"][Configs.GetPlayer()]["stats"]["equipment"]["wasRT"]["repairs"].ToString() + " repairs";
            Equipments.Add(rt);

            Equipment c4 = new Equipment();
            c4.EquipName = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeC4"]["name"].ToString();
            c4.Description = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeC4"]["desc"].ToString();
            c4.EquipPic = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeC4"]["img"].ToString();
            c4.KitPic = "misc/Mp_Support_Sc_Support.png";
            c4.LineTwo = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeC4"]["kills"].ToString() + " kills";
            Equipments.Add(c4);

            Equipment d = new Equipment();
            d.EquipName = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["wasDef"]["name"].ToString();
            d.Description = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["wasDef"]["desc"].ToString();
            d.EquipPic = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["wasDef"]["img"].ToString();
            d.KitPic = "misc/Mp_Assault_Sc_Assault.png";
            d.LineTwo = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["wasDef"]["revives"].ToString() + " revives";
            Equipments.Add(d);

            Equipment tugg = new Equipment();
            tugg.EquipName = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqUGS"]["name"].ToString();
            tugg.Description = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqUGS"]["desc"].ToString();
            tugg.EquipPic = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqUGS"]["img"].ToString();
            tugg.KitPic = "misc/Mp_Recon_Sc_Recon.png";
            tugg.LineTwo = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqUGS"]["spots"].ToString() + " spots";
            Equipments.Add(tugg);

            Equipment mine = new Equipment();
            mine.EquipName = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeMine"]["name"].ToString();
            mine.Description = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeMine"]["desc"].ToString();
            mine.EquipPic = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeMine"]["img"].ToString();
            mine.KitPic = "misc/Mp_Engineer_Sc_Engineer.png";
            mine.LineTwo = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeMine"]["kills"].ToString() + " kills and " +
                data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeMine"]["hits"].ToString() + " hits in " +
                data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeMine"]["shots"].ToString() + " deploys";
            Equipments.Add(mine);

            Equipment clay = new Equipment();
            clay.EquipName = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeClay"]["name"].ToString();
            clay.Description = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeClay"]["desc"].ToString();
            clay.EquipPic = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeClay"]["img"].ToString();
            clay.KitPic = "misc/Mp_Support_Sc_Support.png";
            clay.LineTwo = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeClay"]["kills"].ToString() + " kills and " +
                data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeClay"]["hits"].ToString() + " hits in " +
                data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeClay"]["shots"].ToString()+ " deploys";
            Equipments.Add(clay);

            Equipment billy = new Equipment();
            billy.EquipName = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqEOD"]["name"].ToString();
            billy.Description = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqEOD"]["desc"].ToString();
            billy.EquipPic = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqEOD"]["img"].ToString();
            billy.KitPic = "misc/Mp_Engineer_Sc_Engineer.png";
            billy.LineTwo = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqEOD"]["kills"].ToString() + " kills";
            Equipments.Add(billy);

            Equipment sof = new Equipment();
            sof.EquipName = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqSOF"]["name"].ToString();
            sof.Description = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqSOF"]["desc"].ToString();
            sof.EquipPic = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqSOF"]["img"].ToString();
            sof.KitPic = "misc/Mp_Recon_Sc_Recon.png";
            sof.LineTwo = "";
            Equipments.Add(sof);

            Equipment mort = new Equipment();
            mort.EquipName = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeMort"]["name"].ToString();
            mort.Description = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeMort"]["desc"].ToString();
            mort.EquipPic = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeMort"]["img"].ToString();
            mort.KitPic = "misc/Mp_Support_Sc_Support.png";
            mort.LineTwo = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["waeMort"]["kills"].ToString() + " kills";
            Equipments.Add(mort);

            Equipment mav = new Equipment();
            mav.EquipName = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqMAV"]["name"].ToString();
            mav.Description = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqMAV"]["desc"].ToString();
            mav.EquipPic = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqMAV"]["img"].ToString();
            mav.KitPic = "misc/Mp_Recon_Sc_Recon.png";
            mav.LineTwo = data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqMAV"]["kills"].ToString() + " kills and "+
                data["list"][Configs.GetPlayer()]["stats"]["equipment"]["seqMAV"]["spots"].ToString() + " spots";
            Equipments.Add(mav);

            Equipments.Sort();

            double assaulttime = Convert.ToDouble(Assault.TimeRaw);
            double recontime = Convert.ToDouble(Recon.TimeRaw);
            double supporttime = Convert.ToDouble(Support.TimeRaw);
            double engtime = Convert.ToDouble(Engineer.TimeRaw);
            double totaltime = assaulttime + recontime + supporttime + engtime;
            string assaultper = String.Format("{0:0.##}", ((assaulttime / totaltime) * 100));
            string reconper = String.Format("{0:0.##}", ((recontime / totaltime) * 100));
            string supportper = String.Format("{0:0.##}", ((supporttime / totaltime) * 100));
            string engper = String.Format("{0:0.##}", ((engtime / totaltime) * 100));
            string timesuri = "http://chart.googleapis.com/chart?&cht=pc&chd=t:100|" + assaultper + "," + engper + "," + supportper + "," + reconper + "&chco=ffffff,308DBF|C4B469|86AE31|A1562E&chs=";
            BackTilePic = timesuri;

            JArray upcomingranks = (JArray)data["list"][Configs.GetPlayer()]["stats"]["nextranks"];
            int k = 0;
            while (k <= upcomingranks.Count - 1)
            {
                JObject u = JObject.Parse(upcomingranks[k].ToString());
                NextRank nr = new NextRank();
                nr.RankName = u["name"].ToString();
                nr.RankPic = u["img_small"].ToString();
                nr.Level = u["nr"].ToString();
                nr.RequiredScore = Convert.ToInt32(u["score"].ToString());
                nr.Left = Convert.ToInt32(u["left"].ToString());
                NextRanks.Add(nr);
                k++;
            }

            Configs.SetPlayerRank(this.Rank);

            IntDateChecked = data["list"][Configs.GetPlayer()]["stats"]["date_check"].ToString();
            IntDateUpdated = data["list"][Configs.GetPlayer()]["stats"]["date_update"].ToString();
        }
    }
}
