// pch.cpp: source file corresponding to the pre-compiled header

#include "pch.h"
#include <cstdio>


bool FindCharCpp(char* str, char c, int n)
{
	while (n>0)
	{
		if (*str == c)
		{
			return true;
		}
		str++;
		n--;
	}
	return false;
	
}


extern"C"
int mainCRTStartup();
bool FindCharCpp(char *str, char c, int n);
extern"C"


int entrypoint()
{
	mainCRTStartup();
	return 0;
}

int main()
{
	return 0;
}
// When you are using pre-compiled headers, this source file is necessary for compilation to succeed.
