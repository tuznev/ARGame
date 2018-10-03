public enum GAME_STATE {
	ATTRACTOR,
	INTRODUCTION_TRANSITION, // let's go
	ZONE_TRANSITION, // motorway - urban - etc
	FEATURE_INTRODUCTION, // ui animation and explanation the feature (technology)
	CAROUSEL,
	EVENT_SELECTION, // on carousel event selected
	EVENT_PLACEMENT, // user choose some road event on specific technology
	END_TRANSITION, // exit from current zone
	ZONE_END_LOOPER // wait for user to push event button
}

public enum TECHNOLOGY {
	// Motorway
	ADAPTIVE_CRUISE_CONTROL = 1, // priority 1
	DCM_DRIVER_CONDITIONER_MONITOR = 4, // priority 4

	// Urban
	EMERGENCY_BRAKING = 2, // priority 2
	SURROUND_360_CAMERA = 3, // priority 3
	BLIND_SPOT_ASSIST = 5, // priority 5

	// Parking
	CLEAR_EXIT = 6 // priority 6
}

public enum ZONE { MOTORWAY, URBAN, PARKING }

public enum DAYTIME { DAY, NIGHT }

public enum WEATHER { SUN, RAIN }

public enum LANGUAGE { ENGLISH, FRENCH }

public enum ROAD_EVENT {
	//! Motorway 
	// Adaptive Cruise Control
	Traffic_slows_ACC,
	Jaguar_e_Trophy_ACC,
	Traffic_jam_gridlock_ACC,
	// DCM Driver Conditioner Monitor  - animation
	Latte_DCM,
	CoffeeAndSnooze_DCM,
	DoubleEspresso_DCM,
	//! Urban
	// Emergency braking
	Traffic_slows_EB,
	Cyclist_EB,
	Junction_EB,
	// Blind spot assist
	Ordinary_vehicle_passes_BSA,
	Juggernaut_passes_BSA,
	Emergency_vehicle_passes_BSA,
	//! Parking
	// 360 surround camera - animation
	Plan_view_CAM360,
	Junction_view_CAM360,
	Kerb_view_CAM360,
	// Clear Exit
	Ordinary_vehicle_passes_CEM,
	Formula_E_I_Type_passes_CEM,
	Emergency_vehicle_passes_CEM,

	None
}

public enum ACTION_STEP { NONE, INTRO, MAIN, OUTRO }

//public class Statics {
//	public static string[] events_names = {
//		// ACC - motorway
//		"Traffic Slows",
//		"eTrophy Race",
//		"Traffic Jam",

//		//BSA - urban
//		"Ordinary vehicle passes",
//		"Juggernaut passes",
//		"Emergency vehicle passes",	

//		// CEM - parking 
//		"Ordinary vehicle passes",
//		"Formula E I-Type passes",
//		"Emergency vehicle passes",

//		// DCM - motorway
//		"Driver drifts",
//		"Driver Speed Creeps Down",
//		"Driver Speed Creeps Up",

//		// EB - urban  
//		"Traffic slows",
//		"Cyclist",
//		"Junction",

//		// 360 SURROUND CAMERA - parking 
//		"Plan view",
//		"Junction view",
//		"Kerb view"
//	};
//}