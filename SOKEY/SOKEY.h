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

#define SURFACE_WIDTH   64 			//��ͼ�õ�source face��//
#define SURFACE_HEIGHT  48			//��ͼ�õ�source face��//
#define CG_INVISIBLE				99
#define MAP_READ_FLAG	0x8000		// ???????????????????
#define MAP_SEE_FLAG	0x4000		// ??????��??????????
#define DISP_BUFFER_SIZE 4096 	// ����???????

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
	EVENT_NONE,			// ?????�D��???
	EVENT_NPC,			// ?�eNPC
	EVENT_ENEMY,		// ?�e��??????
	EVENT_WARP,			// ???
	EVENT_DOOR,			// ??
	EVENT_ALTERRATIVE,	// ??????�l????????????��???
	EVENT_WARP_MONING,	// ��?�e???
	EVENT_WARP_NOON,	// ??�e???
	EVENT_WARP_NIGHT,	// ??�e???

	EVENT_END		// ????�k????
};

typedef struct {
	int x, y;				//��ͼʱ������// ����?��
	int bmpNo;			//ͼ���
	ACTION *pAct;		// ����
	BOOL hitFlag;		// ʮλ��Ϊ 1:Ҫ��ʾalpha 2:���ʹ��� 3:ʯ�� 4:�ж�	
	char DrawEffect;	// 0:���ر��� 1:alpha 2:���ʹ��� 3:ʯ�� 4:�ж�
#ifdef _READ16BITBMP
	char DrawEffect;	// 0:���ر��� 1:alpha 2:���ʹ��� 3:ʯ�� 4:�ж�
#endif
#ifdef _SFUMATO
	int sfumato;		// ������Ⱦͼ��ɫ��
#endif
}DISP_INFO;

typedef struct {
	short no;	//����ͼ��DISP_INFO��λ��// ?????�t??�k?
	UCHAR dispPrio; 	//��ʾʱ������˳��// ����??�I??
}DISP_SORT;

typedef struct {
	DISP_INFO DispInfo[DISP_BUFFER_SIZE];
	DISP_SORT DispSort[DISP_BUFFER_SIZE];
	short 		DispCnt;	//Ŀǰ��������//
}DISP_BUFFER;



DISP_BUFFER DispBuffer;
BOOL MuteFlag = FALSE;

enum{
	DISP_PRIO_BG 			= 0,	//����
	DISP_PRIO_TILE 		= 1,	//�ر�// ????????��????????
	DISP_PRIO_CHAR 		= 10,	//����// ???
	DISP_PRIO_PARTS 	= 10,	//����// ?????��??
	DISP_PRIO_RESERVE = 20,	//Ԥ��
	DISP_PRIO_JIKI 		= 30,	/* ��? 	*/
	DISP_PRIO_GRID 		= 100,	// ????????
	DISP_PRIO_BOX,				/* ???? */
	DISP_PRIO_IME1,				/* ????????????  ?��? */
	DISP_PRIO_IME2,				/* ????????????  ����? */
								/* ??????? */
	DISP_PRIO_MENU,				//ѡ��/* ???? */
	DISP_PRIO_IME3,				/* ?????????????  ?��? */
	DISP_PRIO_IME4,				/* ?????????????  ����? */
	DISP_PRIO_BOX2,				/* ????? */
	DISP_PRIO_ITEM,				/* ???? */
								/* ???????? */
	DISP_PRIO_YES_NO_WND,		/* ?�_????? */
	DISP_PRIO_YES_NO_BTN,		/* ?�_??? */
	DISP_PRIO_BOX3,				/* ????? */
	DISP_PRIO_DRAG,				/* ????? */
	DISP_PRIO_MOUSE,			/* ??????? 	*/
	DISP_PRIO_TOP = 255			/* ??�I 	*/
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