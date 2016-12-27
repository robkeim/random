#!/bin/bash
showlist="showlist"
declare -a shows
shows=("entourage" "burn-notice")
#shows=("house" "smallville" "heroes" "scrubs" "gossip-girl" "stargate-atlantis" "in-plain-sight" "leverage" "how-i-met-your-mother" "burn-notice")
i="0"
set -o nounset

wget --quiet -O $showlist http://eztv.it/index.php?main=showlist

for ((j=0;j<${#shows[*]};j++)); do
	file=$(echo ${shows[${j}]})
	echo -e "\nChecking for new episodes of $file...\n"

	episodelist=$(< $showlist tr " " "\n" | grep "/$file/" | awk -F "[\"]" '{printf $2 "\n"}')

	wget --quiet -O $file http://eztv.it$episodelist
	cat $file | tr " " "\n" | grep "\.torrent" | grep -v "720" | grep -v "1080" | while read line
	do
		let "i+=1"
		if (($i % 2))
		then
			torrent=$(echo $line | awk -F "[\"]" '{printf $2 "\n"}')
			offset=$((`echo $file|sed 's/[^-]//g'|wc -m`+3))
			episode_str=$(echo $torrent | awk -F "[.]" '{printf $'$offset' "\n"}')
#			if (echo $file | grep -q "-")
#			then
#				episode_str=$(echo $torrent | awk -F "[.]" '{printf $5 "\n"}')
#			else
#				episode_str=$(echo $torrent | awk -F "[.]" '{printf $4 "\n"}')
#			fi
#			if (echo $file | grep -q "plain")
#			then
#				episode_str=$(echo $torrent | awk -F "[.]" '{printf $6 "\n"}')
#			fi
			# episode_str now contains one of S##E##, #x##, ##x##
			if (echo $episode_str | grep -q "x")
			then
				# episode_str is one of #x## or ##x##
				season=$(echo $episode_str | awk -F "[x]" '{printf $1 "\n"}')
				episode_s=$(echo $episode_str | awk -F "[x]" '{printf $2 "\n"}')
				if ((${#season} - 1))
				then
					# season is > 1 character in length (##x##)
					episode=$(echo "S${season}E$episode_s")
				else
					# season is exactly 1 character in length (#x##)
					episode=$(echo "S0${season}E$episode_s")
				fi
			else
				# episode_str is already S##E##
				episode=$episode_str
			fi
			#if (find /media/patstore/tv/$file | grep -rqs $episode)
			if (find /home/robkeim/tv/$file | grep -rqs $episode)
			then
#				echo "find /media/patstore/tv/$file | grep -rqs $episode"
				echo "HAVE EPISODE ($episode)"
			else
#				echo "find /media/patstore/tv/$file | grep -rqs $episode"
				echo "DONT HAVE EPISODE ($episode) - Adding torrent ($torrent) to watch directory"
				wget -nc --quiet $torrent
			fi
		fi
	done
	rm -f $file
done
rm -f $showlist
