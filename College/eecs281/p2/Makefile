all: p2exe

p2exe: main.o functions.o
	g++ main.o functions.o -o p2exe

main.o: main.cpp
	g++ -c main.cpp

functions.o: functions.cpp
	g++ -c functions.cpp
	
submit:
	rm -rf *.o p2exe submission.tar.gz
	tar -zcf submission.tar.gz *

clean:
	rm -rf *.o p2exe

