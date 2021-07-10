// SACreateACT.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include<iostream>
#include<stdio.h>
#include<WinSock2.h>
#include<string.h>
#include <atlstr.h>
#include "SACreateACT.h"
#include "iphlpapi.h"
#include "SASO.h"
#pragma comment(lib,"Iphlpapi.lib")


using namespace std;

#pragma comment(lib,"ws2_32.lib")
#pragma warning(disable:4996)



SERVERINFO g_serverinfo;
SOCKET rsocket;
char PersonalKey[4096];
char* DefaultKey;

int main()
{
	strcpy_s(charname, "5gs059");
	ConnectServer();
	strcpy_s(charname, "5gs060");
	ConnectServer();
		//CreateAccount();

    //std::cout << "Hello World!\n";
	return 0;
}

// 运行程序: Ctrl + F5 或调试 >“开始执行(不调试)”菜单
// 调试程序: F5 或调试 >“开始调试”菜单

// 入门使用技巧: 
//   1. 使用解决方案资源管理器窗口添加/管理文件
//   2. 使用团队资源管理器窗口连接到源代码管理
//   3. 使用输出窗口查看生成输出和其他消息
//   4. 使用错误列表窗口查看错误
//   5. 转到“项目”>“添加新项”以创建新的代码文件，或转到“项目”>“添加现有项”以将现有代码文件添加到项目
//   6. 将来，若要再次打开此项目，请转到“文件”>“打开”>“项目”并选择 .sln 文件


int ConnectServer()
{
	int port = 7022;
	int nRet = -1;
	BOOL bConnected = FALSE;
	char* serverKey = (char*)malloc(150 * sizeof(char));

	WORD sockVersion = MAKEWORD(2, 2);
	WSADATA data;
	if (WSAStartup(sockVersion, &data) != 0) {

		return 0;
	}

	rsocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	//套接字创建失败！
	if (rsocket == INVALID_SOCKET) {

		printf("invalid socket!\n");
		return -1;
	}
	//石器电信9线shiqi.so|43.227.220.123|9071|3
	sockaddr_in servAddr;
	memset(&servAddr, 0, sizeof(servAddr));			//每个字节都用0填充
	servAddr.sin_family = AF_INET;// AF_INET;
	servAddr.sin_port = htons(port);
	servAddr.sin_addr.s_addr = inet_addr("43.227.196.21");

	connect(rsocket, (SOCKADDR*)&servAddr, sizeof(SOCKADDR));

	//接收服务器消息
	char szBuffer[MAXBYTE] = { 0 };
	recv(rsocket, szBuffer, MAXBYTE, NULL);

	//输出接收到的数据
	cout << "服务端：" << szBuffer << "\r\n" << endl;

	
	CheckUser(charname, "xiaohuilili", szBuffer);
	CreateNewChar(0, "cangku00x", 100035, 30175, 10, 0, 0, 10, 5, 5, 0, 0, 1);
	return 1;
}


//ret = CheckUser(user.charname, user.password, serverKey);
void CheckUser(char* cdkey, char* pwd, char* serverkey)
{
	char buffer[8192], raw[8192], result[8192], message[8192];
	int func, fieldcount, checksum = 0, checksumrecv;
	int recvbytes;

	CString strtmp(serverkey);
	CString str = strtmp.Right(strtmp.GetLength() - 1);
	SASO* so = new SASO(str);
	char* SOKey = so->RunningKey();
	DefaultKey = SOKey;


	//发送用户名和密码
	ZeroMemory(buffer, sizeof(buffer));
	strcpy_s(PersonalKey, _RUNNING_KEY);
	checksum += util_mkstring(buffer, cdkey);
	checksum += util_mkstring(buffer, pwd);

	char* nicInfo = (char*)malloc(150 * sizeof(char));
	memset(nicInfo, 0, 150 * sizeof(char));
	//GetNicInfoAddress(&*nicInfo);
	checksum += util_mkstring(buffer, nicInfo);
	int x = 1;

	checksum += util_mkint(buffer, x);
	//strcpy_s(message, nicInfo);

	util_mkint(buffer, checksum);
	if (!util_SendMesg(&rsocket, 71, buffer)) {
		printf("SENDMSG_ERROR");
		return ;
	}
	ZeroMemory(buffer, sizeof(buffer));
	recvbytes = recv(rsocket, buffer, sizeof(buffer), 0);
	if (recvbytes < 8 || strlen(buffer) != recvbytes) {
		recvbytes = recv(rsocket, buffer, sizeof(buffer), 0);
		if (recvbytes < 8 || strlen(buffer) != recvbytes) {
			printf("RECVMSG_ERROR");
			return ;
		}
	}

	
	//检测用户名和密码是否正确
	//autil.util_Init();
	util_Init_RuntimeKey();
	util_DecodeMessage(raw, buffer);
	if (!util_SplitMessage(raw, SEPARATOR))
	{
		printf("SPLITMSG_ERROR");
		return ;
	}
	if (!util_GetFunctionFromSlice(&func, &fieldcount)) {
		printf("GETFUNC_ERROR");
		return;
	}
	checksum = 0;
	if (func == 82 && fieldcount == 2) {
		checksum += util_destring(2, result);
		util_deint(3, &checksumrecv);
		if (checksum != checksumrecv) {
			printf("CHECKSUM_ERROR");
			return;
		}
		if (strcmp(result, "ok") != 0 && strcmp(result, "successful") != 0) {
			printf("CDKEY_AND_PWD_ERROR");
			return;
		}
	}
	else if (func == 92 && fieldcount == 3) {
		checksum += util_destring(2, result);
		checksum += util_destring(3, message);
		util_deint(4, &checksumrecv);
		if (checksum != checksumrecv)
		{
			printf("CHECKSUM_ERROR");
			return;
		}
		if (strcmp(result, "failed") == 0)
		{
			printf("NOT_LOGGED_IN");
			return;
		}
	}
	else {
		printf("INVALID_INFO");
		return ;
	}
	//向服务端发送接收就绪信息
	checksum = 0;
	ZeroMemory(buffer, sizeof(buffer));
	util_mkint(buffer, checksum);
	if (!util_SendMesg(&rsocket, 79, buffer)) {
		printf("SENDMSG_ERROR");
		return ;
	}
	ZeroMemory(buffer, sizeof(buffer));
	recvbytes = recv(rsocket, buffer, sizeof(buffer), 0);
	if (recvbytes <= 0)
	{
		printf("RECVMSG_ERROR");
		return ;
	}

	//未创建人物
	if (strlen(message) == 0)
	{
		printf("NOACCOUNT");
		return;
	}
		
}


/// <summary>
/// //CreateNewChar(0,user.charname,100035,30175,10,10,0,0,0,0,0,10,1)!=SUCCESSFUL
/// </summary>
/// <param name="dataplace"></param>
/// <param name="charname"></param>
/// <param name="imgno"></param>
/// <param name="faceimgno"></param>
/// <param name="vital"></param>
/// <param name="str"></param>
/// <param name="tgh"></param>
/// <param name="dex"></param>
/// <param name="earth"></param>
/// <param name="water"></param>
/// <param name="fire"></param>
/// <param name="wind"></param>
/// <param name="hometown"></param>
void CreateNewChar(int dataplace, char* charname, int imgno, int faceimgno, int vital, int str, int tgh, int dex, int earth, int water, int fire, int wind, int hometown) {
	char buffer[8192], raw[8192], result[8192], message[8192];
	int func, fieldcount, checksum = 0, checksumrecv;
	int recvbytes;

	//向服务端发送创建新人物信息
	checksum = 0;
	ZeroMemory(buffer, sizeof(buffer));
	checksum += util_mkint(buffer, dataplace);
	checksum += util_mkstring(buffer, charname);
	checksum += util_mkint(buffer, imgno);
	checksum += util_mkint(buffer, faceimgno);
	checksum += util_mkint(buffer, vital);
	checksum += util_mkint(buffer, str);
	checksum += util_mkint(buffer, tgh);
	checksum += util_mkint(buffer, dex);
	checksum += util_mkint(buffer, earth);
	checksum += util_mkint(buffer, water);
	checksum += util_mkint(buffer, fire);
	checksum += util_mkint(buffer, wind);
	checksum += util_mkint(buffer, hometown);
	
	util_mkint(buffer, checksum);
	if (!util_SendMesg(&rsocket, 73, buffer)) {
		printf("IsOnLine=FALSE;");
		return;
	}
	ZeroMemory(buffer, sizeof(buffer));
	recvbytes = recv(rsocket, buffer, sizeof(buffer), 0);
	if (recvbytes <= 0){
		printf("RECVMSG_ERROR");
		return;
	}
	
	//检测新帐号创建是否成功
	util_DecodeMessage(raw, buffer);
	if (!util_SplitMessage(raw, SEPARATOR)) {
		printf("GETFUNC_ERROR");
		return;
	}
		
	if (!util_GetFunctionFromSlice(&func, &fieldcount)) {
		printf("GETFUNC_ERROR");
		return;
	}
	checksum = 0;
	if (func == 84 && fieldcount == 3) {
		checksum += util_destring(2, result);
		checksum += util_destring(3, message);
		util_deint(4, &checksumrecv);
		if (checksum != checksumrecv) {
			printf("CHECKSUM_ERROR");
			return;
		}
		if (strcmp(result, "successful") != 0) {
			printf("CHECKSUM_ERROR");
			return;
		}
	}
	else if (func == 82 && fieldcount == 2) {
		checksum += util_destring(2, result);
		checksum += util_destring(2, message);
		printf("\n");
		printf(message);
	}
	else
	{
		printf("INVALID_INFO");
		return;
	}

	
	return;
}

void CreatUser()
{
	
}

int util_mkint(char* buffer, int value)
{
	int t1, t2;
	char t3[4096];

	if (strlen(PersonalKey) == 0)
		strcpy(PersonalKey, DefaultKey);

	util_swapint(&t1, &value, "3142");
	t2 = t1 ^ 0xffffffff;
	util_256to64_shr(t3, (char*)&t2, sizeof(int), DEFAULTTABLE, PersonalKey);
	strcat(buffer, ";");
	strcat(buffer, t3);

	return value;
}

void util_swapint(int* dst, int* src, char* rule)
{
	char* ptr, * qtr;
	int i;

	ptr = (char*)src;
	qtr = (char*)dst;
	for (i = 0; i < 4; i++) qtr[rule[i] - '1'] = ptr[i];
}

int util_256to64_shr(char* dst, char* src, int len, char* table, char* key)
{
	unsigned int dw, dwcounter, i, j;

	if (!dst || !src || !table || !key) return 0;
	if (strlen(key) < 1) return 0;	// key can't be empty.
	dw = 0;
	dwcounter = 0;
	j = 0;
	for (i = 0; i < len; i++) {
		dw = (((unsigned int)src[i] & 0xff) << ((i % 3) * 2)) | dw;
		dst[dwcounter++] = table[((dw & 0x3f) + key[j]) % 64];	// check!
		j++;  if (!key[j]) j = 0;
		dw = (dw >> 6);
		if (i % 3 == 2) {
			dst[dwcounter++] = table[((dw & 0x3f) + key[j]) % 64];// check!
			j++;  if (!key[j]) j = 0;
			dw = 0;
		}
	}
	if (dw) dst[dwcounter++] = table[(dw + key[j]) % 64];	// check!
	dst[dwcounter] = '\0';
	return dwcounter;
}
int util_mkstring(char* buffer, char* value)
{
	char t1[SLICE_SIZE];

	if (strlen(PersonalKey) == 0)
		strcpy(PersonalKey, DefaultKey);

	util_256to64_shl(t1, value, strlen(value), DEFAULTTABLE, PersonalKey);
	strcat(buffer, ";");	// It's important to append a SEPARATOR between fields
	strcat(buffer, t1);

	return strlen(value);
}
int util_256to64_shl(char* dst, char* src, int len, char* table, char* key)
{
	unsigned int dw, dwcounter, i, j;

	if (!dst || !src || !table || !key) return 0;
	if (strlen(key) < 1) return 0;	// key can't be empty.
	dw = 0;
	dwcounter = 0;
	j = 0;
	for (i = 0; i < len; i++) {
		dw = (((unsigned int)src[i] & 0xff) << ((i % 3) * 2)) | dw;
		dst[dwcounter++] = table[((dw & 0x3f) + 64 - key[j]) % 64];	// check!
		j++;  if (!key[j]) j = 0;
		dw = (dw >> 6);
		if (i % 3 == 2) {
			dst[dwcounter++] = table[((dw & 0x3f) + 64 - key[j]) % 64];	// check!
			j++;  if (!key[j]) j = 0;
			dw = 0;
		}
	}
	if (dw) dst[dwcounter++] = table[(dw + 64 - key[j]) % 64];	// check!
	dst[dwcounter] = '\0';
	return dwcounter;
}
BOOL util_SendMesg(SOCKET* soc, int func, char* buffer)
{
	//char t1[16384], t2[16384];
	char t1[1024 * 64], t2[1024 * 64];
	int ret, nSize;
	sprintf(t1, "&;%d%s;#;", func + 13, buffer);
	util_EncodeMessage(t2, t1);
	nSize = strlen(t2);
	t2[nSize] = 10;
	nSize += 1;
	ret = send(*soc, t2, nSize, 0);
	if (ret == nSize)
		return TRUE;
	else
		return FALSE;
}
void util_EncodeMessage(char* dst, char* src)
{
	//  strcpy(dst, src);
	//  util_xorstring(dst, src);

	int rn = rand() % 99;
	int t1, t2;
	char t3[65500], tz[65500];

	util_swapint(&t1, &rn, "2413");
	t2 = t1 ^ 0xffffffff;
	util_256to64(tz, (char*)&t2, sizeof(int), DEFAULTTABLE);

	util_shlstring(t3, src, rn);
	strcat(tz, t3);
	util_xorstring(dst, tz);
}
int util_256to64(char* dst, char* src, int len, char* table)
{
	unsigned int dw, dwcounter, i;

	if (!dst || !src || !table) return 0;
	dw = 0;
	dwcounter = 0;
	for (i = 0; i < len; i++) {
		dw = (((unsigned int)src[i] & 0xff) << ((i % 3) * 2)) | dw;
		dst[dwcounter++] = table[dw & 0x3f];
		dw = (dw >> 6);
		if (i % 3 == 2) {
			dst[dwcounter++] = table[dw & 0x3f];
			dw = 0;
		}
	}
	if (dw) dst[dwcounter++] = table[dw];
	dst[dwcounter] = '\0';
	return dwcounter;
}
void util_shlstring(char* dst, char* src, int offs)
{
	char* ptr;
	if (!dst || !src || (strlen(src) < 1)) return;

	offs = offs % strlen(src);
	ptr = src + offs;
	strcpy(dst, ptr);
	strncat(dst, src, offs);
	dst[strlen(src)] = '\0';
}
void util_xorstring(char* dst, char* src)
{
	int i;
	if (strlen(src) > 65500) return;

	//DebugPoint=100000;

	for (i = 0; i < strlen(src); i++) {
		//DebugPoint=100000+i;
		dst[i] = src[i] ^ 255;
	}
	dst[i] = '\0';
	//DebugPoint=1000;
}

void GetNicInfoAddress(char* nicInfo)
{
	PIP_ADAPTER_INFO pAdapterInfo;
	PIP_ADAPTER_INFO pAdapter = NULL;
	DWORD dwRetVal = 0;
	UINT i;

	struct  tm newtime;
	char buffer[32];
	errno_t error;

	ULONG ulOutBufLen = sizeof(PIP_ADAPTER_INFO);
	pAdapterInfo = (IP_ADAPTER_INFO*)MALLOC(sizeof(IP_ADAPTER_INFO));
	if (pAdapterInfo == NULL)
	{
		return;
	}

	if (GetAdaptersInfo(pAdapterInfo, &ulOutBufLen) == ERROR_BUFFER_OVERFLOW)
	{
		FREE(pAdapterInfo);
		pAdapterInfo = (IP_ADAPTER_INFO*)MALLOC(ulOutBufLen);
		if (pAdapterInfo == NULL)
		{
			return;
		}
	}

	char* tmp = (char*)malloc(150 * sizeof(char));
	memset(tmp, 0, 150 * sizeof(char));
	if ((dwRetVal = GetAdaptersInfo(pAdapterInfo, &ulOutBufLen)) == NO_ERROR)
	{
		pAdapter = pAdapterInfo;
		for (i = 0; i < pAdapter->AddressLength; i++)
		{
			if (i == (pAdapter->AddressLength - 1))
			{
				sprintf(tmp, "%.2X\n", (int)pAdapter->Address[i]);
			}
			else
			{
				sprintf(tmp, "%.2X-", (int)pAdapter->Address[i]);
			}
			strcat(nicInfo, tmp);
		}
	}

	if (pAdapterInfo)
	{
		FREE(pAdapter);
	}

}
BOOL util_Init_RuntimeKey(void)
{
	int i;

	for (i = 0; i < SLICE_MAX; i++) {
		memset(MesgSlice[i], 0, SLICE_SIZE);
	}
	SliceCount = 0;
	ZeroMemory(PersonalKey, sizeof(PersonalKey));
	strcpy_s(PersonalKey, charname);
	strcat_s(PersonalKey, sizeof(PersonalKey) / sizeof(char), DefaultKey);
	return TRUE;
}

void util_DecodeMessage(char* dst, char* src)
{
	//  strcpy(dst, src);
	//  util_xorstring(dst, src);

#define INTCODESIZE	(sizeof(int)*8+5)/6

	int rn;
	int* t1, t2;
	char t3[4096], t4[4096];	// This buffer is enough for an integer.
	char tz[65500];

	if (src[strlen(src) - 1] == '\n') src[strlen(src) - 1] = '\0';
	util_xorstring(tz, src);

	// get seed
	strncpy(t4, tz, INTCODESIZE);
	t4[INTCODESIZE] = '\0';
	util_64to256(t3, t4, DEFAULTTABLE);
	t1 = (int*)t3;
	t2 = *t1 ^ 0xffffffff;
	util_swapint(&rn, &t2, "3142");

	util_shrstring(dst, tz + INTCODESIZE, rn);
}

int util_64to256(char* dst, char* src, char* table)
{
	unsigned int dw, dwcounter, i;
	char* ptr = NULL;

	dw = 0;
	dwcounter = 0;
	if (!dst || !src || !table) return 0;
	for (i = 0; i < strlen(src); i++) {
		ptr = (char*)index(table, src[i]);
		if (!ptr) return 0;
		if (i % 4) {
			dw = ((unsigned int)(ptr - table) & 0x3f) << ((4 - (i % 4)) * 2) | dw;
			dst[dwcounter++] = dw & 0xff;
			dw = dw >> 8;
		}
		else {
			dw = (unsigned int)(ptr - table) & 0x3f;
		}
	}
	if (dw) dst[dwcounter++] = dw & 0xff;
	dst[dwcounter] = '\0';
	return dwcounter;
}
void util_shrstring(char* dst, char* src, int offs)
{
	char* ptr;
	if (!dst || !src || (strlen(src) < 1)) return;

	offs = strlen(src) - (offs % strlen(src));
	ptr = src + offs;
	strcpy(dst, ptr);
	strncat(dst, src, offs);
	dst[strlen(src)] = '\0';
}

BOOL util_SplitMessage(char* source, char* separator)
{
	if (source && separator) {	// NULL input is invalid.
		char* ptr;
		char* head = source;

		// Nuke 1006 : Bug fix
		while ((ptr = (char*)strstr(head, separator)) && (SliceCount < SLICE_MAX) && (SliceCount >= 0)) {
			ptr[0] = '\0';
			if (strlen(head) < SLICE_SIZE) {	// discard slices too large

				// Nuke 0701
				//		if (*MesgSlice != *dumb) {
				//print("Warning! Mem may be broken\n");
				//}
				/*
				if (MesgSlice[SliceCount]==0xffffffff) {
				print("MesgSlice[%d] broken\n",SliceCount);
				return FALSE;
				} else {
				*/
				strcpy(MesgSlice[SliceCount], head);
				SliceCount++;
				//}

			}
			head = ptr + 1;
		}
		strcpy(source, head);	// remove splited slices	
	}
	return TRUE;
}

int util_GetFunctionFromSlice(int* func, int* fieldcount)
{
	char t1[16384];
	int i;

	//  if (strcmp(MesgSlice[0], DEFAULTFUNCBEGIN)!=0) util_DiscardMessage();

	strcpy(t1, MesgSlice[1]);
	// Robin adjust
	//*func=atoi(t1);
	*func = atoi(t1) - 13;
	for (i = 0; i < SLICE_MAX; i++)
		if (strcmp(MesgSlice[i], DEFAULTFUNCEND) == 0) {
			*fieldcount = i - 2;	// - "&" - "#" - "func" 3 fields
			return 1;
		}

	return 0;	// failed: message not complete
}
int util_deint(int sliceno, int* value)
{
	int* t1, t2;
	char t3[4096];	// This buffer is enough for an integer.

	if (strlen(PersonalKey) == 0)
		strcpy(PersonalKey, DefaultKey);

	util_shl_64to256(t3, MesgSlice[sliceno], DEFAULTTABLE, PersonalKey);
	t1 = (int*)t3;
	t2 = *t1 ^ 0xffffffff;

	util_swapint(value, &t2, "2413");

	return *value;
}
int util_shl_64to256(char* dst, char* src, char* table, char* key)
{
	unsigned int dw, dwcounter, i, j;
	char* ptr = NULL;

	if (!key || (strlen(key) < 1)) return 0;	// must have key

	dw = 0;
	dwcounter = 0;
	j = 0;
	if (!dst || !src || !table) return 0;
	for (i = 0; i < strlen(src); i++) {
		ptr = (char*)index(table, src[i]);
		if (!ptr) return 0;
		if (i % 4) {
			// check!
			dw = ((((unsigned int)(ptr - table) & 0x3f) + 64 - key[j]) % 64) << ((4 - (i % 4)) * 2) | dw;
			j++;  if (!key[j]) j = 0;
			dst[dwcounter++] = dw & 0xff;
			dw = dw >> 8;
		}
		else {
			// check!
			dw = (((unsigned int)(ptr - table) & 0x3f) + 64 - key[j]) % 64;
			j++;  if (!key[j]) j = 0;
		}
	}
	if (dw) dst[dwcounter++] = dw & 0xff;
	dst[dwcounter] = '\0';
	return dwcounter;
}int util_destring(int sliceno, char* value)
{

	if (strlen(PersonalKey) == 0)
		strcpy(PersonalKey, DefaultKey);

	util_shr_64to256(value, MesgSlice[sliceno], DEFAULTTABLE, PersonalKey);

	return strlen(value);
}
int util_shr_64to256(char* dst, char* src, char* table, char* key)
{
	unsigned int dw, dwcounter, i, j;
	char* ptr = NULL;

	if (!key || (strlen(key) < 1)) return 0;	// must have key

	dw = 0;
	dwcounter = 0;
	j = 0;
	if (!dst || !src || !table) return 0;
	for (i = 0; i < strlen(src); i++) {
		ptr = (char*)index(table, src[i]);
		if (!ptr) return 0;
		if (i % 4) {
			// check!
			dw = ((((unsigned int)(ptr - table) & 0x3f) + key[j]) % 64) << ((4 - (i % 4)) * 2) | dw;
			//dw=(((unsigned int)(ptr-table)+key[j]&0xff)%64)<<((4-(i%4))*2) | dw;
			j++;  if (!key[j]) j = 0;
			dst[dwcounter++] = dw & 0xff;
			dw = dw >> 8;
		}
		else {
			// check!
			dw = (((unsigned int)(ptr - table) & 0x3f) + key[j]) % 64;
			//dw=((unsigned int)(ptr-table)+key[j]&0xff)%64;
			j++;  if (!key[j]) j = 0;
		}
	}
	if (dw) dst[dwcounter++] = dw & 0xff;
	dst[dwcounter] = '\0';
	return dwcounter;
}
char* index(char* table, char src)
{
	char* p = table;
	while (*p != src) {
		p++;
	}
	return p;
}