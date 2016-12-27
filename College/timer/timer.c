#include <sys/time.h>
#include <iostream>

using namespace std;

int main(int argc, char** argv){
string command;
for(int i = 1; i < argc; i++){
command += argv[i];
command += " ";
}

timeval start;
timeval end;

gettimeofday(&start, NULL);
system(command.c_str());
gettimeofday(&end, NULL);

cout << 1000000*(end.tv_sec - start.tv_sec) + end.tv_usec - start.tv_usec << endl;

return 0;
} 
