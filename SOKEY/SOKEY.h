#pragma once
typedef struct {
	unsigned char atari_x, atari_y;
	unsigned short hit;
	short height;
	short broken;
	short indamage;
	short outdamage;
	short inpoison;
	short innumb;
	short inquiet;
	short instone;
	short indark;
	short inconfuse;
	short outpoison;
	short outnumb;
	short outquiet;
	short outstone;
	short outdark;
	short outconfuse;
	short effect1;
	short effect2;
	unsigned short damy_a;
	unsigned short damy_b;
	unsigned short damy_c;
	unsigned int bmpnumber;
} MAP_ATTR;

struct ADRNBIN {
	unsigned long	bitmapno;
	unsigned long	adder;
	unsigned long	size;
	int	xoffset;
	int	yoffset;
	unsigned int width;
	unsigned int height;
	MAP_ATTR attr;
};

#define MAP_TILE_GRID_X1	-20
#define MAP_TILE_GRID_X2	+17		
#define MAP_TILE_GRID_Y1	-16
#define MAP_TILE_GRID_Y2	+21		
#define MAP_X_SIZE	(MAP_TILE_GRID_X2 - MAP_TILE_GRID_X1)
#define MAP_Y_SIZE	(MAP_TILE_GRID_Y2 - MAP_TILE_GRID_Y1)
#define GRID_SIZE		64

#define SURFACE_WIDTH   64 			//存图用的source face宽//
#define SURFACE_HEIGHT  48			//存图用的source face高//
#define CG_INVISIBLE				99
#define MAP_READ_FLAG	0x8000		// ???????????????????
#define MAP_SEE_FLAG	0x4000		// ????????????????
#define DISP_BUFFER_SIZE 4096 	// ???????

unsigned short tile[MAP_X_SIZE * MAP_Y_SIZE];	
unsigned short parts[MAP_X_SIZE * MAP_Y_SIZE];	
unsigned short event[MAP_X_SIZE * MAP_Y_SIZE];
unsigned short hitMap[MAP_X_SIZE * MAP_Y_SIZE];
int DEF_APPSIZEX = 800;
int DEF_APPSIZEY = 600;
int SCREEN_WIDTH_CENTER = DEF_APPSIZEX / 2;
int SCREEN_HEIGHT_CENTER = DEF_APPSIZEY / 2;

typedef unsigned char MOJI;
typedef unsigned char U1;
typedef          char S1;
typedef unsigned short U2;
typedef          short S2;
typedef unsigned long U4;
typedef          long S4;
typedef float  F4;
typedef double F8;

int draw_map_bgm_flg = 0;

typedef struct action ACTION;

enum
{
	EVENT_NONE,			// ????????
	EVENT_NPC,			// ?NPC
	EVENT_ENEMY,		// ???????
	EVENT_WARP,			// ???
	EVENT_DOOR,			// ??
	EVENT_ALTERRATIVE,	// ?????????????????????
	EVENT_WARP_MONING,	// ????
	EVENT_WARP_NOON,	// ?????
	EVENT_WARP_NIGHT,	// ?????

	EVENT_END		// ????????
};

typedef struct {
	int x, y;				//秀图时的座标// ?
	int bmpNo;			//图编号
	ACTION *pAct;		// 动作
	BOOL hitFlag;		// 十位数为 1:要显示alpha 2:饱和处理 3:石化 4:中毒	
	char DrawEffect;	// 0:无特别处理 1:alpha 2:饱和处理 3:石化 4:中毒
#ifdef _READ16BITBMP
	char DrawEffect;	// 0:无特别处理 1:alpha 2:饱和处理 3:石化 4:中毒
#endif
#ifdef _SFUMATO
	int sfumato;		// 二次渲染图层色彩
#endif
}DISP_INFO;

typedef struct {
	short no;	//这张图在DISP_INFO的位置// ????????
	UCHAR dispPrio; 	//显示时的优先顺序// ????
}DISP_SORT;

typedef struct {
	DISP_INFO DispInfo[DISP_BUFFER_SIZE];
	DISP_SORT DispSort[DISP_BUFFER_SIZE];
	short 		DispCnt;	//目前储存数量//
}DISP_BUFFER;



DISP_BUFFER DispBuffer;
BOOL MuteFlag = FALSE;

enum{
	DISP_PRIO_BG 			= 0,	//背景
	DISP_PRIO_TILE 		= 1,	//地表// ????????䴘????????
	DISP_PRIO_CHAR 		= 10,	//人物// ???
	DISP_PRIO_PARTS 	= 10,	//建物// ???????
	DISP_PRIO_RESERVE = 20,	//预留
	DISP_PRIO_JIKI 		= 30,	/* ? 	*/
	DISP_PRIO_GRID 		= 100,	// ????????
	DISP_PRIO_BOX,				/* ???? */
	DISP_PRIO_IME1,				/* ????????????  ?? */
	DISP_PRIO_IME2,				/* ????????????  ? */
								/* ??????? */
	DISP_PRIO_MENU,				//选单/* ???? */
	DISP_PRIO_IME3,				/* ?????????????  ?? */
	DISP_PRIO_IME4,				/* ?????????????  ? */
	DISP_PRIO_BOX2,				/* ????? */
	DISP_PRIO_ITEM,				/* ???? */
								/* ???????? */
	DISP_PRIO_YES_NO_WND,		/* ?????? */
	DISP_PRIO_YES_NO_BTN,		/* ???? */
	DISP_PRIO_BOX3,				/* ????? */
	DISP_PRIO_DRAG,				/* ????? */
	DISP_PRIO_MOUSE,			/* ??????? 	*/
	DISP_PRIO_TOP = 255			/* ?? 	*/
};




int MAP();
void initMap();
int readMap(int floor, int x1, int y1, int x2, int y2, unsigned short *tile, unsigned short *parts, unsigned short *event);
void drawMap(void);
void readHitMap(int x1, int y1, int x2, int y2, unsigned short *tile, unsigned short *parts, unsigned short *event, unsigned short *hitMap);
void camMapToGamen(float sx, float sy, float *ex, float *ey);
BOOL realGetNo(U4 CharAction, U4 *GraphicNo);
BOOL realGetHitFlag(U4 GraphicNo, S2 *Hit);
BOOL realGetHitPoints(U4 GraphicNo, S2 *HitX, S2 *HitY);
BOOL checkEmptyMap(int dir);
void checkAreaLimit(short *x1, short *y1, short *x2, short *y2);
int StockDispBuffer(int x, int y, UCHAR dispPrio, int bmpNo, BOOL hitFlag);
BOOL realGetPos(U4 GraphicNo, S2 *x, S2 *y);
int play_environment(int tone, int x, int y);
int play_map_bgm(int tone);
BOOL realGetWH(U4 GraphicNo, S2 *w, S2 *h);
BOOL realGetPrioType(U4 GraphicNo, S2 *prioType);
void setPartsPrio(int graNo, int x, int y, int dx, int dy, float mx, float my, int dispPrio);