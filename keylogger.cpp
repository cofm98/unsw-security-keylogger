#include <iostream>
#include <windows.h>
#include <conio.h>
#include <ctype.h>

using namespace std;

int main ()
{
    while(true)
    {
        for(int n = 65; n < 65 + 26; n++)
        {
            SHORT msg;
            if(msg = GetAsyncKeyState(n) & 0x0001)
            {
                int n1 = n - 65;
                char msgout = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[n1];
                cout<<"Key pressed: "<<msgout<<endl;
                Sleep(10);
            }
        }
    }
    return 0;
}