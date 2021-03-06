// SOKEY.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <string>
#include <iostream>
#include <fstream>
#include "SASO.h"
#include "SOKEY.h"
using namespace std;

#define OLD_GRAPHICS_START	1000000		// 原本realbin图的最大量
#define MAX_GRAPHICS	 	OLD_GRAPHICS_START	// 最大图量// ?????
#define SEARCH_AREA		11		// ????????????????????
#define AUTO_MAPPING_W		54
#define AUTO_MAPPING_H		54


ADRNBIN adrnbuff[MAX_GRAPHICS];
unsigned long bitmapnumbertable[MAX_GRAPHICS];

int nowFloor;
int nowFloorGxSize, nowFloorGySize;
int nowGx, nowGy;
float nowX = (float)nowGx*GRID_SIZE, nowY = (float)nowGy * GRID_SIZE;
float nowVx, nowVy, nowSpdRate;
int nextGx, nextGy;
int oldGx = -1, oldGy = -1;
int oldNextGx = -1, oldNextGy = -1;
int mapAreaWidth, mapAreaHeight;
int mapAreaX1, mapAreaY1, mapAreaX2, mapAreaY2;

float viewPointX;
float viewPointY;
int viewOffsetX = SCREEN_WIDTH_CENTER;
int viewOffsetY = SCREEN_HEIGHT_CENTER;
int nowXFastDraw, nowYFastDraw;
int baseXFastDraw, baseYFastDraw;
int amountXFastDraw2 = 0, amountYFastDraw2 = 0;
int amountXFastDraw = 0, amountYFastDraw = 0;
int nowXFastDraw2, nowYFastDraw2;
int baseXFastDraw2, baseYFastDraw2;
BOOL mapEmptyFlag;
short getMapAreaCnt;
short getMapAreaX1[2], getMapAreaY1[2], getMapAreaX2[2], getMapAreaY2[2];
short mapEmptyDir;
int mapEmptyGx, mapEmptyGy;
BOOL autoMappingInitFlag = TRUE;
unsigned char autoMappingBuf[AUTO_MAPPING_H][AUTO_MAPPING_W];
int charPrioCnt;


enum
{
	CHAR_PARTS_PRIO_TYPE_CHAR,
	CHAR_PARTS_PRIO_TYPE_PARTS,
	CHAR_PARTS_PRIO_TYPE_ANI
};

typedef struct TAG_CHAR_PARTS_PRIORITY
{
	unsigned int graNo;
	int x, y;
	int dx, dy;
	int depth;
	float mx, my;
	short type;
	TAG_CHAR_PARTS_PRIORITY *pre;
	TAG_CHAR_PARTS_PRIORITY *next;
#ifdef _SFUMATO
	int sfumato;
#endif
} CHAR_PARTS_PRIORITY;


#define MAX_CHAR_PRIO_BUF	2048
CHAR_PARTS_PRIORITY charPrioBufTop;
CHAR_PARTS_PRIORITY charPrioBuf[MAX_CHAR_PRIO_BUF];

void addCharPartsPrio(CHAR_PARTS_PRIORITY *, CHAR_PARTS_PRIORITY *);
BOOL checkPrioPartsVsChar(CHAR_PARTS_PRIORITY *ptc, CHAR_PARTS_PRIORITY *ptp);
void insertCharPartsPrio(CHAR_PARTS_PRIORITY *pt1, CHAR_PARTS_PRIORITY *pt2);


int main()
{
	//F573E2F0B1676B630
	//F4F2690A43D1CFF19"
	//"F63A29AE807C74E18"

	//FD03727596E0364F1
	//F573E2F0B1676B630



	/*
	CString strtmp = "F573E2F0B1676B630";
	CString str = strtmp.Right(strtmp.GetLength() - 1);
	SASO *so = new SASO(str);
	char *SOKey = (char*)malloc(150 * sizeof(char));
	memset(SOKey, 0, 8);
	SOKey = so->RunningKey();
	printf("\n");
	printf(SOKey);


	SASO *so1 = new SASO(str);
	strtmp = "F573E2F0B1676B630";
	str = strtmp.Right(strtmp.GetLength() - 1);
	memset(SOKey, 0, 8);
	SOKey = so1->RunningKey();

	printf("\n");
	printf(SOKey);*/

	initMap();

	printf("\n");
	return 1;



	//////////////////////////////////////////////////////////////////////////////////////////////////
	FILE *fp;
	char floorname[255];
	int i = 0, j, k, ox, oy;
	short l = 0;
	int fl;
	int maxx = 64;
	int maxy = 47;
	int damy_a;

	fl = 2003;

	if (fl == 0)
		return 1;

	sprintf(floorname, "%d.dat", fl);

	
	

	
	

	
	//std::cout << SOKey;
}

int MAP()
{
	int flen;
	char *p;
	int fWidth;
	int fHeight;
	int fOffset;
	fOffset = sizeof(int) << 1;
	FILE *fp;
	errno_t nError;
	double dbnum[100];
	nError = fopen_s(&fp, "adrn_138.bin", "rb");
	//nError = fopen_s(&fp, "D:\\GAME\\shiqiso70\\shiqiso\\map\\2003.dat", "rb");
	//nError = fopen_s(&fp, "D:\\GAME\\Runing\\1_100\\map\\2003.dat", "rb");


	int m_long;
	m_long = sizeof("adrn_138.bin");
	FILE *Addrbinfp;
	ADRNBIN tmpadrnbuff;
	char *addrbinfilename = new char[m_long];
	strcpy(addrbinfilename, "adrn_138.bin");

	if ((Addrbinfp = fopen("adrn_138.bin", "rb")) == NULL)
		return 0;
	int i = 0;
	while (!feof(Addrbinfp)) {
		fread(&tmpadrnbuff, sizeof(tmpadrnbuff), 1, Addrbinfp);
		adrnbuff[tmpadrnbuff.bitmapno] = tmpadrnbuff;

		if (tmpadrnbuff.attr.bmpnumber == 2000)
		{
			printf("%d\n", 1);
		}

		if (tmpadrnbuff.attr.bmpnumber != 0) {
			if ((12802 <= tmpadrnbuff.attr.bmpnumber && tmpadrnbuff.attr.bmpnumber <= 12811)
				|| (10132 <= tmpadrnbuff.attr.bmpnumber && tmpadrnbuff.attr.bmpnumber <= 10136)) {

			}
			if (tmpadrnbuff.attr.bmpnumber <= 33 && tmpadrnbuff.bitmapno > 230000) {//防堵魔法图号覆盖声音的bug
				continue;
			}
			bitmapnumbertable[tmpadrnbuff.attr.bmpnumber] = tmpadrnbuff.bitmapno;
		}
		else
			bitmapnumbertable[tmpadrnbuff.attr.bmpnumber] = 0;

		i++;
	}
	printf("%d\n", i);
	fclose(Addrbinfp);



	if (fp != NULL)
	{
		for (int i = 0; i < 30; i++)
		{

			fseek(fp, i, SEEK_SET);
			fread(&fWidth, sizeof(int), 1, fp);
			printf("%d\n", fWidth);
			//fread(&fHeight, sizeof(int), 1, fp);
		}
	}

	fclose(fp);
}


int readMap(int floor, int x1, int y1, int x2, int y2, unsigned short *tile, unsigned short *parts, unsigned short *event)
{
	FILE *fp;
	char filename[255];
	int fWidth, fHeight, fOffset, mWidth, width, height, fx, fy, mx, my, len, len2, i;

	sprintf_s(filename, "%d.dat", floor);
	if ((fp = fopen(filename, "rb")) != NULL)
	{
		memset(tile, 0, MAP_X_SIZE * MAP_Y_SIZE * sizeof(short));
		memset(parts, 0, MAP_X_SIZE * MAP_Y_SIZE * sizeof(short));
		memset(event, 0, MAP_X_SIZE * MAP_Y_SIZE * sizeof(short));

		fseek(fp, 0, SEEK_SET);

		fread(&fWidth, sizeof(int), 1, fp);
		fread(&fHeight, sizeof(int), 1, fp);


		mWidth = x2 - x1;
		width = mWidth;
		height = y2 - y1;
		mx = 0;
		fx = x1;

		//printf("mWidth=%d width=%d  height=%d  fx=%d\n",mWidth,width,height,fx);
		if (x1 < 0)
		{
			width += x1;
			fx = 0;
			mx -= x1;
		}
		if (x2 > fWidth)
			width -= (x2 - fWidth);
		my = 0;
		fy = y1;
		if (y1 < 0)
		{
			height += y1;
			fy = 0;
			my -= y1;
		}
		if (y2 > fHeight)
			height -= (y2 - fHeight);

		fOffset = sizeof(int) * 2;
		len = fy * fWidth + fx;
		len2 = my * mWidth + mx;

		for (i = 0; i < height; i++)
		{
			fseek(fp, sizeof(short) * len + fOffset, SEEK_SET);
			fread(&tile[len2], sizeof(short) * width, 1, fp);
			len += fWidth;
			len2 += mWidth;
		}
		fOffset += sizeof(short) * (fWidth * fHeight);
		len = fy * fWidth + fx;
		len2 = my * mWidth + mx;
		for (i = 0; i < height; i++)
		{
			fseek(fp, sizeof(short) * len + fOffset, SEEK_SET);
			fread(&parts[len2], sizeof(short) * width, 1, fp);//aaaaaaaaaaaaa
			len += fWidth;
			len2 += mWidth;
		}
		fOffset += sizeof(short) * (fWidth * fHeight);
		len = fy * fWidth + fx;
		len2 = my * mWidth + mx;
		for (i = 0; i < height; i++)
		{
			fseek(fp, sizeof(short) * len + fOffset, SEEK_SET);
			fread(&event[len2], sizeof(short) * width, 1, fp);
			len += fWidth;
			len2 += mWidth;
		}
		fclose(fp);
	}

	return 1;
}


void initMap()
{
	nowFloor = 2002;
	nowFloorGxSize = 0;
	nowFloorGySize = 0;
	nowGx = 0;
	nowGy = 0;
	nowX = 0;
	nowY = 0;
	nextGx = 0;
	nextGy = 0;
	oldGx = 0, oldGy = 0;
	oldNextGx = 0, oldNextGy = 0;

	mapAreaX1 = nowGx + MAP_TILE_GRID_X1;
	mapAreaY1 = nowGy + MAP_TILE_GRID_Y1;
	mapAreaX2 = nowGx + MAP_TILE_GRID_X2;
	mapAreaY2 = nowGy + MAP_TILE_GRID_Y2;


	mapAreaWidth = mapAreaX2 - mapAreaX1;
	mapAreaHeight = mapAreaY2 - mapAreaY1;
	nowVx = 0;
	nowVy = 0;

	drawMap();
	
}


void drawMap(void)
{
	int i, j, x, y, tx, ty, rainFlag = 0, snowFlag = 0, tryFlag = 0;;
	S2 xx, yy, ww, hh;
	float dx, dy;
	U4 bmpNo;

	draw_map_bgm_flg = 0;

	//	readMap(nowFloor, mapAreaX1, mapAreaY1, mapAreaX2, mapAreaY2, &tile[0], &parts[0], &event[0]);readMap(nowFloor, mapAreaX1, mapAreaY1, mapAreaX2, mapAreaY2, &tile[0], &parts[0], &event[0]);

	camMapToGamen(0.0, 0.0, &dx, &dy);
	baseXFastDraw = (int)(dx + .5);
	baseYFastDraw = (int)(dy + .5);
	nowXFastDraw = baseXFastDraw;
	nowYFastDraw = baseYFastDraw;


	if (readMap(nowFloor, mapAreaX1, mapAreaY1, mapAreaX2, mapAreaY2, &tile[0], &parts[0], &event[0]))
	{
		// hitMap[]????????
		readHitMap(mapAreaX1, mapAreaY1, mapAreaX2, mapAreaY2, &tile[0], &parts[0], &event[0], &hitMap[0]);
		if (mapEmptyFlag)
		{
			if (!checkEmptyMap(mapEmptyDir))
			{
				mapEmptyFlag = FALSE;
				autoMappingInitFlag = TRUE;	// ?????????
			}
		}
		else
			autoMappingInitFlag = TRUE;	// ?????????
//			readMapAfterFrame = 0;	// ????????????????????????
	}
 	else
		return;



	nowXFastDraw2 = baseXFastDraw;
	nowYFastDraw2 = baseYFastDraw;
	amountXFastDraw = 0;
	amountYFastDraw = 0;
	// 1 5 12 16 ... 
	tx = nowXFastDraw2 + (mapAreaX1 + mapAreaY2 - 1) * SURFACE_WIDTH / 2;
	ty = nowYFastDraw2 + (-mapAreaX1 + mapAreaY2 - 1) * SURFACE_HEIGHT / 2;

	int ti, tj;

	ti = mapAreaHeight - 1;
	tj = 0;

	while (ti >= 0)
	{
		i = ti;
		j = tj;
		x = tx;
		y = ty;
		//		if (i==30)
		while (i >= 0 && j >= 0)
		{
			// ???
			if (tile[i * mapAreaWidth + j] > CG_INVISIBLE)
			{
#if 0
				// ????????(???)
				if (193 <= tile[i * mapAreaWidth + j] && tile[i * mapAreaWidth + j] <= 196)
					play_environment(0, x, y);
#endif
				// ???????
				if (x >= (-SURFACE_WIDTH >> 1) && x < DEF_APPSIZEX + (SURFACE_WIDTH >> 1) &&
					y >= (-SURFACE_HEIGHT >> 1) && y < DEF_APPSIZEY + (SURFACE_HEIGHT >> 1))
					StockDispBuffer(x, y, DISP_PRIO_TILE, tile[i * mapAreaWidth + j], 0);
			}
			else
			{
				// ?????????
				// ??????????
				if (20 <= tile[i * mapAreaWidth + j] && tile[i * mapAreaWidth + j] <= 39)
					play_environment(tile[i * mapAreaWidth + j], x, y);
				else if (40 <= tile[i * mapAreaWidth + j] && tile[i * mapAreaWidth + j] <= 59)// ???????????
				{
					play_map_bgm(tile[i * mapAreaWidth + j]);
					draw_map_bgm_flg = 1;
				}
			}
			// ???
			if (parts[i * mapAreaWidth + j] > CG_INVISIBLE)
			{
#if 0
				// ????????(???)
				if (parts[i * mapAreaWidth + j] == 10011)
					play_environment(2, x, y);
				else if (parts[i * mapAreaWidth + j] == 10012)
					play_environment(1, x, y);
				else if (parts[i * mapAreaWidth + j] == 10203)
					play_environment(4, x, y);
				else if (parts[i * mapAreaWidth + j] == 10048)
				{
					play_map_bgm(2);
					draw_map_bgm_flg = 1;
				}
#endif
				realGetNo(parts[i * mapAreaWidth + j], &bmpNo);
				// ???????
				realGetPos(bmpNo, &xx, &yy);
				realGetWH(bmpNo, &ww, &hh);
				xx += x;
				yy += y;
				if (xx < DEF_APPSIZEX && xx + ww - 1 >= 0 && yy < DEF_APPSIZEY && yy + hh - 1 >= 0)
					// ?????????
					setPartsPrio(bmpNo, x, y, 0, 0, (float)(mapAreaX1 + j) * GRID_SIZE, (float)(mapAreaY1 + i) * GRID_SIZE, -1);
			}
			else
			{
				// ?????????
				// ??????????
				if (20 <= parts[i * mapAreaWidth + j] && parts[i * mapAreaWidth + j] <= 39)
					play_environment(parts[i * mapAreaWidth + j], x, y);
				else if (40 <= parts[i * mapAreaWidth + j] && parts[i * mapAreaWidth + j] <= 59)// ???????????
				{
					play_map_bgm(parts[i * mapAreaWidth + j]);
					draw_map_bgm_flg = 1;
				}
			}
			i--;
			j--;
			x -= SURFACE_WIDTH;
		}
		if (tj < mapAreaWidth - 1)
		{
			tj++;
			tx += SURFACE_WIDTH >> 1;
			ty -= SURFACE_HEIGHT >> 1;
		}
		else
		{
			ti--;
			tx -= SURFACE_WIDTH >> 1;
			ty -= SURFACE_HEIGHT >> 1;
		}
	}

}


void camMapToGamen(float sx, float sy, float *ex, float *ey)
{
	float x0, y0, x, y, tx = (float)(SURFACE_WIDTH >> 1), ty = (float)(SURFACE_HEIGHT >> 1);

	x0 = (sx - viewPointX) / GRID_SIZE;
	y0 = (sy - viewPointY) / GRID_SIZE;
	x = +x0 * tx + y0 * tx;
	y = -x0 * ty + y0 * ty;

	*ex = x + viewOffsetX;
	*ey = y + viewOffsetY;
}

void readHitMap(int x1, int y1, int x2, int y2, unsigned short *tile, unsigned short *parts, unsigned short *event, unsigned short *hitMap)
{
	int width, height, i, j, k, l;
	S2 hit, hitX, hitY;
	U4 bmpNo;

	memset(hitMap, 0, MAP_X_SIZE * MAP_Y_SIZE * sizeof(short));

	width = x2 - x1;
	height = y2 - y1;
	if (width < 1 || height < 1)
		return;

	// ???
	for (i = 0; i < height; i++)
	{
		for (j = 0; j < width; j++)
		{
			// ???????
			if (tile[i * width + j] > CG_INVISIBLE || (60 <= tile[i * width + j] && tile[i * width + j] <= 79))
			{
				realGetNo(tile[i * width + j], &bmpNo);
				// ?????
				realGetHitFlag(bmpNo, &hit);
				// ????????????
				if (hit == 0 && hitMap[i * width + j] != 2)
					hitMap[i * width + j] = 1;
				else if (hit == 2) // hit?2?????????
					hitMap[i * width + j] = 2;
			}
			else
			{
				// 0??11??????????????
				switch (tile[i * width + j])
				{
				case 0:	// 0.bmp(???)????????????
					// ????????????????????
					if ((event[i * width + j] & MAP_SEE_FLAG) == 0)
						break;
				case 1:
				case 2:
				case 5:
				case 6:
				case 9:
				case 10:
					// ?????????????
					if (hitMap[i * width + j] != 2)
						hitMap[i * width + j] = 1;
					break;

				case 4:
					hitMap[i * width + j] = 2;
					break;
				}
			}
		}
	}

	// ???
	for (i = 0; i < height; i++)
	{
		for (j = 0; j < width; j++)
		{
			// ???????
			if (parts[i * width + j] > CG_INVISIBLE)
			{
				realGetNo(parts[i * width + j], &bmpNo);
				// ?????
				realGetHitFlag(bmpNo, &hit);
				// ????????????
				if (hit == 0)
				{
					realGetHitPoints(bmpNo, &hitX, &hitY);
					for (k = 0; k < hitY; k++)
					{
						for (l = 0; l < hitX; l++)
						{
							if ((i - k) >= 0 && (j + l) < width && hitMap[(i - k) * width + j + l] != 2)
								hitMap[(i - k) * width + j + l] = 1;
						}
					}
				}
				// ?????????????????
				// ?????????
				else if (hit == 2)
				{
					realGetHitPoints(bmpNo, &hitX, &hitY);
					for (k = 0; k < hitY; k++)
					{
						for (l = 0; l < hitX; l++)
						{
							if ((i - k) >= 0 && (j + l) < width)
								hitMap[(i - k) * width + j + l] = 2;
						}
					}
				}
				else if (hit == 1 && parts[i * width + j] >= 15680 && parts[i * width + j] <= 15732)
				{
					realGetHitPoints(bmpNo, &hitX, &hitY);
					for (k = 0; k < hitY; k++)
					{
						for (l = 0; l < hitX; l++)
						{
							//if ((i - k) >= 0 && (j + l) < width)
								//hitMap[(i-k)*width+j+l] = 0;
							if (k == 0 && l == 0)
								hitMap[(i - k) * width + j + l] = 1;
						}
					}
				}
			}
			else if (60 <= parts[i * width + j] && parts[i * width + j] <= 79)
			{
				realGetNo(parts[i * width + j], &bmpNo);
				// ?????
				realGetHitFlag(bmpNo, &hit);
				// ????????????
				if (hit == 0 && hitMap[i * width + j] != 2)
					hitMap[i * width + j] = 1;
				// hit?2?????????
				else if (hit == 2)
					hitMap[i * width + j] = 2;
			}
			else
			{
				// 0??11??????????????
				switch (parts[i * width + j])
				{
				case 1:
				case 2:
				case 5:
				case 6:
				case 9:
				case 10:
					// ?????????????
					if (hitMap[i * width + j] != 2)
						hitMap[i * width + j] = 1;
					break;

				case 4:
					hitMap[i * width + j] = 2;
					break;
				}
			}

			// ???????????????
			if ((event[i * width + j] & 0x0fff) == EVENT_NPC)
				hitMap[i * width + j] = 1;
		}
	}
}

BOOL realGetNo(U4 CharAction, U4 *GraphicNo)
{
#ifndef _READ16BITBMP
	if (CharAction < 0 || CharAction >= MAX_GRAPHICS) { *GraphicNo = 0; return FALSE; }
	*GraphicNo = bitmapnumbertable[CharAction];
	return TRUE;
#else
	if (CharAction < 0) {
		*GraphicNo = 0;
		return FALSE;
	}
	if (CharAction >= OLD_GRAPHICS_START) {
		if (CharAction >= MAX_GRAPHICS) {
			*GraphicNo = 0;
			return FALSE;
		}
		*GraphicNo = CharAction;
		return TRUE;
	}
	else *GraphicNo = bitmapnumbertable[CharAction];
	return TRUE;
#endif
}

BOOL realGetHitFlag(U4 GraphicNo, S2 *Hit)
{
	if (GraphicNo < 0 || GraphicNo >= MAX_GRAPHICS) {
		*Hit = 0;
		return FALSE;
	}

	if ((GraphicNo >= 369715 && GraphicNo <= 369847) || GraphicNo == 369941)//强制地表可走
		*Hit = 1;
	else if (GraphicNo >= 369641 && GraphicNo <= 369654)
		*Hit = 1;
	else
		*Hit = (adrnbuff[GraphicNo].attr.hit % 100);

	return TRUE;
}

BOOL realGetHitPoints(U4 GraphicNo, S2 *HitX, S2 *HitY)
{
	if (GraphicNo < 0 || GraphicNo >= MAX_GRAPHICS) { *HitX = 0; *HitY = 0; return FALSE; }

	*HitX = adrnbuff[GraphicNo].attr.atari_x;
	*HitY = adrnbuff[GraphicNo].attr.atari_y;

	return TRUE;
}


BOOL checkEmptyMap(int dir)
{
	// ????????11??????????????TRUE??
	int i, gx, gy, tx, ty, len;
	BOOL flag = FALSE;

	if (mapAreaWidth < MAP_X_SIZE || mapAreaHeight < MAP_Y_SIZE)
		return FALSE;

	getMapAreaCnt = 0;

	if (dir == 0 || dir == 1 || dir == 2)
	{
		gx = nowGx - SEARCH_AREA;
		gy = nowGy - SEARCH_AREA;
		tx = -SEARCH_AREA - MAP_TILE_GRID_X1;
		ty = -SEARCH_AREA - MAP_TILE_GRID_Y1;
		len = (SEARCH_AREA << 1) + 1;
		for (i = 0; i < len; i++)
		{
			if ((0 <= gx && gx < nowFloorGxSize) && (0 <= gy && gy < nowFloorGySize))
			{
				if (event[ty * mapAreaWidth + tx] == 0)
				{
					getMapAreaX1[getMapAreaCnt] = gx - 1;
					getMapAreaY1[getMapAreaCnt] = gy - 1;
					getMapAreaX2[getMapAreaCnt] = gx + 1;
					getMapAreaY2[getMapAreaCnt] = gy + (SEARCH_AREA << 1) + 1;
					checkAreaLimit(&getMapAreaX1[getMapAreaCnt], &getMapAreaY1[getMapAreaCnt], &getMapAreaX2[getMapAreaCnt], &getMapAreaY2[getMapAreaCnt]);
					getMapAreaCnt++;
					flag = TRUE;
					break;
				}
			}
			gy++;
			ty++;
		}
	}
	if (dir == 2 || dir == 3 || dir == 4)
	{
		gx = nowGx - SEARCH_AREA;
		gy = nowGy - SEARCH_AREA;
		tx = -SEARCH_AREA - MAP_TILE_GRID_X1;
		ty = -SEARCH_AREA - MAP_TILE_GRID_Y1;
		len = (SEARCH_AREA << 1) + 1;
		for (i = 0; i < len; i++)
		{
			if ((0 <= gx && gx < nowFloorGxSize) && (0 <= gy && gy < nowFloorGySize))
			{
				if (event[ty * mapAreaWidth + tx] == 0)
				{
					getMapAreaX1[getMapAreaCnt] = gx - 1;
					getMapAreaY1[getMapAreaCnt] = gy - 1;
					getMapAreaX2[getMapAreaCnt] = gx + (SEARCH_AREA << 1) + 1;
					getMapAreaY2[getMapAreaCnt] = gy + 1;
					checkAreaLimit(&getMapAreaX1[getMapAreaCnt], &getMapAreaY1[getMapAreaCnt], &getMapAreaX2[getMapAreaCnt], &getMapAreaY2[getMapAreaCnt]);
					getMapAreaCnt++;
					flag = TRUE;
					break;
				}
			}
			gx++;
			tx++;
		}
	}
	if (dir == 4 || dir == 5 || dir == 6)
	{
		gx = nowGx + SEARCH_AREA;
		gy = nowGy - SEARCH_AREA;
		tx = SEARCH_AREA - MAP_TILE_GRID_X1;
		ty = -SEARCH_AREA - MAP_TILE_GRID_Y1;
		len = (SEARCH_AREA << 1) + 1;
		for (i = 0; i < len; i++)
		{
			if ((0 <= gx && gx < nowFloorGxSize) && (0 <= gy && gy < nowFloorGySize))
			{
				if (event[ty * mapAreaWidth + tx] == 0)
				{
					getMapAreaX1[getMapAreaCnt] = gx;
					getMapAreaY1[getMapAreaCnt] = gy - 1;
					getMapAreaX2[getMapAreaCnt] = gx + 2;
					getMapAreaY2[getMapAreaCnt] = gy + (SEARCH_AREA << 1) + 1;
					checkAreaLimit(&getMapAreaX1[getMapAreaCnt], &getMapAreaY1[getMapAreaCnt], &getMapAreaX2[getMapAreaCnt], &getMapAreaY2[getMapAreaCnt]);
					getMapAreaCnt++;
					flag = TRUE;
					break;
				}
			}
			gy++;
			ty++;
		}
	}
	if (dir == 6 || dir == 7 || dir == 0)
	{
		gx = nowGx - SEARCH_AREA;
		gy = nowGy + SEARCH_AREA;
		tx = -SEARCH_AREA - MAP_TILE_GRID_X1;
		ty = SEARCH_AREA - MAP_TILE_GRID_Y1;
		len = (SEARCH_AREA << 1) + 1;
		for (i = 0; i < len; i++)
		{
			if ((0 <= gx && gx < nowFloorGxSize) && (0 <= gy && gy < nowFloorGySize))
			{
				if (event[ty * mapAreaWidth + tx] == 0)
				{
					getMapAreaX1[getMapAreaCnt] = gx - 1;
					getMapAreaY1[getMapAreaCnt] = gy;
					getMapAreaX2[getMapAreaCnt] = gx + (SEARCH_AREA << 1) + 1;
					getMapAreaY2[getMapAreaCnt] = gy + 2;
					checkAreaLimit(&getMapAreaX1[getMapAreaCnt], &getMapAreaY1[getMapAreaCnt], &getMapAreaX2[getMapAreaCnt], &getMapAreaY2[getMapAreaCnt]);
					getMapAreaCnt++;
					flag = TRUE;
					break;
				}
			}
			gx++;
			tx++;
		}
	}

	return flag;
}

void checkAreaLimit(short *x1, short *y1, short *x2, short *y2)
{
	if (*x1 < 0)
		*x1 = 0;
	if (*y1 < 0)
		*y1 = 0;
	if (*x2 > nowFloorGxSize)
		*x2 = nowFloorGxSize;
	if (*y2 > nowFloorGySize)
		*y2 = nowFloorGySize;
}


//**************************************************************************/
// 	??：	??????????????
// 	??：	UCHAR dispPrio：????????
//		  	int x, int y：坐标
//			int bmpNo：图片号
//			int chr_no：???????
//			int pat_no：?????
//**************************************************************************/
// 储存所有要播放的Image
int StockDispBuffer(int x, int y, UCHAR dispPrio, int bmpNo, BOOL hitFlag)
{
	short dx, dy;
	int BmpNo;

	DISP_SORT 	*pDispSort = DispBuffer.DispSort + DispBuffer.DispCnt;
	DISP_INFO 	*pDispInfo = DispBuffer.DispInfo + DispBuffer.DispCnt;

	if (DispBuffer.DispCnt >= DISP_BUFFER_SIZE) return -2;

	if (-1 <= bmpNo && bmpNo <= CG_INVISIBLE) return -2;

	if (bmpNo > CG_INVISIBLE) {
		realGetNo(bmpNo, (U4 *)&BmpNo);
		realGetPos(BmpNo, &dx, &dy);
	}
	else {
		dx = 0;
		dy = 0;
		BmpNo = bmpNo;
	}

	pDispSort->dispPrio = dispPrio;
	pDispSort->no = DispBuffer.DispCnt;
	pDispInfo->hitFlag = hitFlag;
	if (hitFlag >= 20 && hitFlag < 30) {
		pDispInfo->DrawEffect = 2;
		pDispInfo->hitFlag = hitFlag - 20;
	}
	else if (hitFlag >= 30 && hitFlag < 40) {
		pDispInfo->DrawEffect = 3;
		pDispInfo->hitFlag = hitFlag - 30;
	}
	else if (hitFlag >= 40 && hitFlag < 50) {
		pDispInfo->DrawEffect = 4;
		pDispInfo->hitFlag = hitFlag - 40;
	}
	else pDispInfo->DrawEffect = 0;

	pDispInfo->x = x + dx;
	pDispInfo->y = y + dy;
	pDispInfo->bmpNo = BmpNo;
	pDispInfo->pAct = NULL;
	return DispBuffer.DispCnt++;
}

BOOL realGetPos(U4 GraphicNo, S2 *x, S2 *y)
{
#ifndef _READ16BITBMP
	if (GraphicNo < 0 || GraphicNo >= MAX_GRAPHICS) { *x = 0; *y = 0; return FALSE; }
	*x = adrnbuff[GraphicNo].xoffset;
	*y = adrnbuff[GraphicNo].yoffset;
#else
	if (GraphicNo < 0) { *x = 0; *y = 0; return FALSE; }
	if (GraphicNo >= OLD_GRAPHICS_START) {
		if (GraphicNo > MAX_GRAPHICS) { *x = 0; *y = 0; return FALSE; }
		*x = adrntruebuff[GraphicNo - OLD_GRAPHICS_START].xoffset;
		*y = adrntruebuff[GraphicNo - OLD_GRAPHICS_START].yoffset;
	}
	else {
		*x = adrnbuff[GraphicNo].xoffset;
		*y = adrnbuff[GraphicNo].yoffset;
	}
#endif
	return TRUE;
}

int play_environment(int tone, int x, int y)
{
	return 0;
}

int play_map_bgm(int tone)
{
	return 1;
}

BOOL realGetWH(U4 GraphicNo, S2 *w, S2 *h)
{
#ifndef _READ16BITBMP
	if (GraphicNo < 0 || GraphicNo >= MAX_GRAPHICS) { *w = 0; *h = 0; return FALSE; }
	*w = adrnbuff[GraphicNo].width;
	*h = adrnbuff[GraphicNo].height;
#else
	if (GraphicNo < 0) { *w = 0; *h = 0; return FALSE; }
	if (GraphicNo >= OLD_GRAPHICS_START) {
		if (GraphicNo > MAX_GRAPHICS) { *w = 0; *h = 0; return FALSE; }
		*w = adrntruebuff[GraphicNo - OLD_GRAPHICS_START].width;
		*h = adrntruebuff[GraphicNo - OLD_GRAPHICS_START].height;
	}
	else {
		*w = adrnbuff[GraphicNo].width;
		*h = adrnbuff[GraphicNo].height;
	}
#endif

	return TRUE;
}

void setPartsPrio(int graNo, int x, int y, int dx, int dy, float mx, float my, int dispPrio)
{
	int i;
	CHAR_PARTS_PRIORITY *ptc, *prePtc;
	BOOL flag;

	// ??????????
	if (charPrioCnt >= MAX_CHAR_PRIO_BUF)
		return;

	// ?????????????
	charPrioBuf[charPrioCnt].graNo = graNo;
	charPrioBuf[charPrioCnt].x = x;
	charPrioBuf[charPrioCnt].y = y;
	charPrioBuf[charPrioCnt].dx = dx;
	charPrioBuf[charPrioCnt].dy = dy;
	charPrioBuf[charPrioCnt].mx = mx;
	charPrioBuf[charPrioCnt].my = my;
	if (dispPrio == 0)
		charPrioBuf[charPrioCnt].type = CHAR_PARTS_PRIO_TYPE_ANI;
	else
		charPrioBuf[charPrioCnt].type = CHAR_PARTS_PRIO_TYPE_PARTS;
	charPrioBuf[charPrioCnt].pre = NULL;
	charPrioBuf[charPrioCnt].next = NULL;
	charPrioBuf[charPrioCnt].depth = y;
#ifdef _SFUMATO
	charPrioBuf[charPrioCnt].sfumato = 0;
#endif
	if (charPrioCnt == 0)
		addCharPartsPrio(&charPrioBufTop, &charPrioBuf[charPrioCnt]);
	else
	{
		// ?????????????
		flag = FALSE;
		ptc = charPrioBufTop.next;
		for (i = 0; i < charPrioCnt && ptc != NULL; i++, ptc = ptc->next)
		{
			if (ptc->type == CHAR_PARTS_PRIO_TYPE_CHAR)
			{
				if (checkPrioPartsVsChar(ptc, &charPrioBuf[charPrioCnt]))
				{
					insertCharPartsPrio(ptc, &charPrioBuf[charPrioCnt]);
					flag = TRUE;
					break;
				}
			}
			prePtc = ptc;
		}
		if (!flag)
			addCharPartsPrio(prePtc, &charPrioBuf[charPrioCnt]);
	}
	charPrioCnt++;
}

void addCharPartsPrio(CHAR_PARTS_PRIORITY *pt1, CHAR_PARTS_PRIORITY *pt2)
{
	if (pt1 == NULL || pt2 == NULL)
		return;

	pt2->pre = pt1;
	pt2->next = pt1->next;
	if (pt1->next != NULL)
		(pt1->next)->pre = pt2;
	pt1->next = pt2;
}

BOOL checkPrioPartsVsChar(CHAR_PARTS_PRIORITY *ptc, CHAR_PARTS_PRIORITY *ptp)
{
	short hit, prioType;
	S2 w, h;

	// ???????????
	realGetPrioType(ptp->graNo, &prioType);
	// ?????
	realGetHitFlag(ptp->graNo, &hit);
	// ??? prioType == 3 ??????
	if (hit != 0 && prioType == 3)
		return FALSE;
	// ????
	//  ????????
/*	if (329585 <= ptp->graNo && ptp->graNo <= 329590)
		return FALSE;*/
	if (prioType == 1)
	{
		if (ptc->mx <= ptp->mx || ptc->my >= ptp->my)
			return FALSE;
		else
			return TRUE;
	}
#if 0
	// ????
	//  ??????????
	else if (prioType == 2)
	{
		// ????????????
		if ((ptc->mx <= ptp->mx && ptc->my >= ptp->my) || (ptc->mx < ptp->mx - GRID_SIZE || ptc->my > ptp->my + GRID_SIZE))
			return FALSE;
		else
			return TRUE;
	}
#endif
	// ??????????????
#if 1
	if (ptc->mx > ptp->mx && ptc->my < ptp->my)
		return TRUE;
	else
	{
		realGetHitPoints(ptp->graNo, &w, &h);
		if (ptc->x > ptp->x)
		{
			// PC???????
			if (ptp->y - (w - 1) * SURFACE_HEIGHT / 2 <= ptc->y)
				return FALSE;
		}
		else if (ptc->x < ptp->x)
		{
			// PC???????
			if (ptp->y - (h - 1) * SURFACE_HEIGHT / 2 <= ptc->y)
				return FALSE;
		}
		else
		{
			if (ptp->y <= ptc->y)
				return FALSE;
		}
	}
#else
	realGetHitPoints(ptp->graNo, &w, &h);
	if (ptc->x >= ptp->x)
	{
		// PC???????
		if (ptp->y - (w - 1) * SURFACE_HEIGHT / 2 < ptc->y)
			return FALSE;
	}
	else
	{
		// PC???????
		if (ptp->y - (h - 1) * SURFACE_HEIGHT / 2 < ptc->y)
			return FALSE;
	}
#endif

	return TRUE;
}

BOOL realGetPrioType(U4 GraphicNo, S2 *prioType)
{
	if (GraphicNo < 0 || GraphicNo >= MAX_GRAPHICS) {
		*prioType = 0;
		return FALSE;
	}

	*prioType = (adrnbuff[GraphicNo].attr.hit / 100);
	return TRUE;
}
void insertCharPartsPrio(CHAR_PARTS_PRIORITY *pt1, CHAR_PARTS_PRIORITY *pt2)
{
	if (pt1 == NULL || pt2 == NULL)
		return;

	pt2->pre = pt1->pre;
	pt2->next = pt1;
	(pt1->pre)->next = pt2;
	pt1->pre = pt2;
}
// 运行程序: Ctrl + F5 或调试 >“开始执行(不调试)”菜单
// 调试程序: F5 或调试 >“开始调试”菜单

// 入门提示: 
//   1. 使用解决方案资源管理器窗口添加/管理文件
//   2. 使用团队资源管理器窗口连接到源代码管理
//   3. 使用输出窗口查看生成输出和其他消息
//   4. 使用错误列表窗口查看错误
//   5. 转到“项目”>“添加新项”以创建新的代码文件，或转到“项目”>“添加现有项”以将现有代码文件添加到项目
//   6. 将来，若要再次打开此项目，请转到“文件”>“打开”>“项目”并选择 .sln 文件
