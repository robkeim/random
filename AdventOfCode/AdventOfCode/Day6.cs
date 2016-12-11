using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public static class Day6
    {
        // http://adventofcode.com/2016/day/6
        //
        // --- Day 6: Signals and Noise ---
        //
        // Something is jamming your communications with Santa.Fortunately, your signal is only partially jammed, and protocol in situations like this is to
        // switch to a simple repetition code to get the message through.
        //
        // In this model, the same message is sent repeatedly. You've recorded the repeating message signal (your puzzle input), but the data seems quite
        // corrupted - almost too badly to recover. Almost.
        //
        // All you need to do is figure out which character is most frequent for each position. For example, suppose you had recorded the following messages:
        //
        // eedadn
        // drvtee
        // eandsr
        // raavrd
        // atevrs
        // tsrnev
        // sdttsa
        // rasrtv
        // nssdts
        // ntnada
        // svetve
        // tesnvt
        // vntsnd
        // vrdear
        // dvrsen
        // enarar
        // The most common character in the first column is e; in the second, a; in the third, s, and so on.Combining these characters returns the error-corrected
        // message, easter.
        //
        // Given the recording in your puzzle input, what is the error-corrected version of the message being sent?
        // bgpmxqws\nmxvdaluh\nwpgcrcix\ndjugxgak\nurjgbqde\nvcsylkay\nruyowwjt\neepdbfaa\nkpzjujdv\nilsbjxbf\nxljdcdpc\nvnmtqzbu\ndsiruyjs\noemrvmqj\nbunqbyjw\nwixyxnwq\ntfmpgyen\nrxjphoyf\nkeohkpwv\ngbpfqodg\npalgwnye\nzzriwene\nwmczggbk\nxikxduml\nestibkpk\nivcghhot\nuvczidij\nqmdmpfxn\nxjgypmry\nxwzgzxeu\nejvoqgyr\nqzbnawul\nmhtvpsma\nvzddmtyx\nckhdphcd\nrrxiqrqd\ntdnauotp\nzsoqslob\noxbleyra\ndfspawmw\namewbjnz\ndhryqzsg\ngevzondd\ngjtlhacs\nywoghawg\nxgiglflw\njlfewwky\nxvhjgvhk\nbaocvjcl\nwnweoaib\nuepsdnur\nvmynttbb\naoqezdgl\noahtcpli\nhixokbhp\nrsdwsjnv\nnqckpjlt\njksheyvr\nasabcisr\njirzozrt\njypcypek\nhbeimsej\nvzwnchwy\nozjqqyaz\nhbxcqvne\nyfmthknj\nyxicfnav\nfhfwaetl\nlyhvemqr\nkyyzkgfb\nunigfcbx\njyszsjto\nqoomixgp\nvwqlvanp\nwteqnjhj\ndtcfvira\nwztxowzh\nrjuayajd\nmcvaqelp\nwazbrrej\nqwuiszub\nfohaxlyl\nvizomswk\npkjhxghs\nxhygtcbp\ngrzjvlas\ntgssuvej\njypumznm\nymiymxbk\nfdnhxmpc\nnqwlpigo\necpyhmgw\nhbxpvgoh\npkflsrjo\nzrjugqge\njwvlowtd\nmkslbbql\nhsektxsi\npsanaqop\nylorypou\ntkfircdx\nftkfzmno\nrdasheam\neulndcvo\ngpetuqvl\nfelsxwks\nxckkdvyc\nnlbymfrt\ngcqxmyse\ngbbnguow\ncdaduwiv\nsqikwrbi\nppwteldh\ntyurlqsr\noogkdood\nslxekxkj\ntunmorwo\nmphktfgp\nylkifdek\nbyopdakn\nzgpqnghe\nlltqwxuf\npalhjqcb\nxxrbywel\nxolycxlx\nbkiimxvx\nhtztessk\naamwgfrm\nextuovqh\nbpkmstaf\nhazuzhkf\ncjiqycqs\nsxafxxpp\nmtfowzth\npoosymcj\nvbykdleq\nbxhhfyak\nkbyminxs\ngviprbxv\nbqxhjffd\nwmuwlzoy\niwazluuu\nuhwxfelq\ngfvqwfgj\nuqvyjkgr\neanwjhhl\nizpooten\ngkqhaiel\njforeagg\nhrqjylmb\nkcvfbohb\njkoskwff\nymuqatcy\nnaeceeru\nalghunmu\nygsgeyxy\nfhsxqtsx\niikldgzj\nzwsdownm\nsvbdvddw\nujwdwmju\npuszwwxg\npwzuivlo\nnoqjcwqu\nlsnvidsn\nrhuvjosk\nrxktxanu\niftbsfjc\nkgrxrkwl\nrfzknqde\nafqcjguq\nsghybsrn\nqtipzcwy\nyyqhuufw\nzaeukrhr\nrqtlcflu\nridxvnur\nwcxzmvka\npqxcddgq\neetnhsux\nkblokhxx\nbmctwlgo\ncpojyilz\nyhnmkhjp\nbtudgpci\nmzvjtlhh\nxabfbuvt\njgoltfpy\ngsjdsaco\nbmxhpnri\nncdkduzl\njhzyshfz\nqhruewva\ncgkafvvm\nntjbaria\nrpfxuhht\nwnqbudow\nbcezvcpt\nnqrhgrkn\ntceyjrqf\npyszfamz\nnctlttmt\nbvhanhoc\nilffiatn\nfwmskxwg\nvezsripn\nhbjxpdyd\ntjnpgdib\ntiuniqdj\nmrzlrnmn\nngqrtjxr\neoyorrfy\nrtkidptz\nzwglnkeu\nanjcxsgc\ndbuotxcq\nsqpsxbmt\nczyxgtcv\nojhmhssl\nlfbhgnox\npecipazx\nnvcfxguk\ngdniujcm\ntcdfhgez\nzxzybtvb\noddtlvmw\nvxdcfivs\nldgbhriu\nlcuccuup\ndwzyuvkh\npdoomnps\njfaqworq\nzfeecwuy\nzxytmbzh\niuzcfqac\nkxvxqpam\ngyfryaqu\ndusowjue\nuwrofbxz\njwhbtsgg\ntjpubrqi\nvncupbao\nlbusnztb\nkbpkhcau\nprrcxgti\naflcpnsq\nxtspjvwl\nfqjgujib\ntnlahhpp\nboovsuro\neytzygmd\nvdyysubn\nsyxcupva\nulgjkkdy\npzhevrme\nvynmztwh\nwrwebmrb\npdfniopo\nnnusdprq\nqzrnxboz\nfhvnyafq\nisbzhjnq\nnxfvcxvy\niuzgpevj\napjpvsdk\nunltokdk\nneccyyrf\ngkkafbth\nudnurvso\ngzosdaws\nsnetsjdd\nzcsgluqb\nfdsvyiho\nkjvmexiu\nugfpphts\ndbkvkdok\ndrpkejfw\nlwyshtxq\nqilaojzn\nqilwixhi\nhbuzdkgg\namdettxe\nrilvspmg\ncyvfwmwg\nkqbmwvvi\nnuxdfinf\nozfvzigf\nvhjvfosm\nvfmgntex\ncswjzkft\nkvqqlvbh\nppwpiqcb\nwewsncdj\nndkjslvd\nlivwaogi\nslupeobk\nchvlbixa\noprvhtpn\nwrgwrzic\nmeyhlwzr\nwcwplger\ncfdwqikc\nvokgnzjv\nsxmxzlwh\nrttwwsrg\napqmweoc\nxcrgliqw\negjenpzi\nuuaoghhw\nubaubqir\nslxfrqfz\nuooravpz\njukdeivx\nqvelgzzi\nzbcnzjsj\nnoivjeht\ntomkyktn\nowixssbg\nqrjikjok\nopieopkj\nhzrratbf\ntawhmgiz\nzojlupqh\niuxrcduy\ngxdvgzke\ntcsqiada\noqanfwxs\nhoeavozw\nkteefzjp\nxjepfoho\nacaimhfz\nifeqkbqc\nszwlvqwc\njrtfkzxv\naqgmlcyg\nstsejxzs\nmukbwojc\nyorhqkqz\nqgkehpbu\nqrpealli\nmcwerdgx\nvqxkeyqe\npmstbkfy\nuriiqytq\npuyrfebm\ncljqpflg\nyhjxqfee\nwvyitlyj\nstvxunze\nsurpasjh\nlaqbwefs\ntmhzxhcp\nxnrmdgci\ncvoziimt\ndbeqiwio\nbncszppp\nwajpsycd\nncmrinrp\nzsctcqzy\nvkfwzoda\nzeturmnd\ndydeylro\ngggkrwzw\noobmkfhz\nvimgaxkq\ncuftcyxg\njzczmzab\njqvvaljj\nfvlbbduo\ntvoipipg\nihdqovcz\nfhylllit\nwngiyeld\npanifluc\nnariaulb\nuiqpccns\nvdeevhcb\nfjddbcfa\nnymgtdmf\nqvkrocra\nlelplbmu\nqwajtxne\ncjgljjwm\nkdyecbii\nrmhccint\nmeclgocv\nahvvcbck\nuktuwuag\ndcnpzwjn\nigfbtmnr\nwdioghpd\nkeuuecam\nclxwiylf\noqsbygex\ndbxhlukg\nrkxjjlvn\nvotomymd\nhqyfigpr\nqnuvattu\nflrxtbsl\nzinwdott\nwxpzgsxk\nglvwrzqv\nasruvcjq\naanwzupj\nkrbxlowc\nnfbrzogr\nivvbjgyt\nmxbuttye\nrhzksroq\nskipgtsv\nwrfnmsgm\nckdgipqw\nuxbylsdi\nbazhagcz\nmelrnxrj\nqqqoyhqf\nhrfjpsrx\nhafnvrdg\nwzasinyu\nfrbkqlzq\npkpasbfh\nuaadlrys\ntjxovpar\nlvqxahjd\nereefqow\ntmwalbhi\neflnfinc\nhwmxucjt\niedrvuyy\nmnmxdbhv\nqghmvftt\nboqbamap\nrjfncukp\njsyshihy\nkwfcnspx\nlsvaiysm\nxlypkceq\nuvpxfarx\nonktnulb\ncrycggor\nhzntxzxs\njodwwaaj\nasmnoijg\nlkgabxtn\nyinohytm\ncymfafvu\npfwnxkga\nfcaepans\ndrpzqntz\ndmtlraxa\nxpuaeobo\nnpwbdnyw\nprgddqif\ncfjaozss\nynarmrcs\nydkoyipv\nrxbjmxfy\nytbnudvq\nrtcauesa\ngwvfttly\nxxocfbov\noepykzhu\nsiaojkqc\nxumasfkb\nmpaimhwd\nlutlhkrm\nhmlmhezr\nhkffbvol\njhnyuwbk\nxfunkzni\ngvhtrpfw\nzqcfnvmn\ncwsftepw\nvfgrhquw\nhspqqeka\nkhebmbyz\nhjinidaa\nljgfvzhi\nbgyfruun\nqhruutty\nonanrpll\nqsfqisbh\noyhlplyf\nmpnbcbjv\ndfjjijeg\nbgbrnyhl\nouybnypn\nciemtumh\nostosnmx\nzxmuoqdf\ntouhjqxz\nuqzlprdk\ntwplkydd\nckfkowhr\nesjrccyf\nebidrjtd\nyakqyhfv\nkgohdxvc\nfmlurdki\nlpzvjdzi\nqgouskfp\njyhqxfft\nlmngjjil\ndiswenna\nawidoqvi\ndpgeemdk\ncyckbeyc\nysxatowo\nmmjigkqq\nlcaaoore\nbbleeyyn\nseuzxpgr\nqthrjfya\nfnkeqtep\nlkykhmgv\nmgosqkkf\notrjzklc\njqmmyvsd\nildfyzld\neubkvrel\ngispnfiv\nhlhoeiif\nshruvvsc\nwoitvqxc\nbmbitfft\nvzrdnbrq\nenxhxgmq\nmmnodgks\niocqmalp\nyklqxeii\npiakutiq\naszalaqn\nlbgkpcnt\nyrwdtvuv\nrtlvmzej\nfwtjkmxe\nxiivpktx\ndqsssnus\nkcbbesoj\nlsqczazm\nfuzgkvig\nizqabubp\nennmupvh\nsjgycnfm\nnemdzypm\nkqqritkb\njpfvpfex\naxqruijm\nfidctvkt\nnzzbxsfp\nuugacsof\nfdgdphbf\nwdvcnnts\neqtzaieh\nazkgkmmv\nzawfrbzx\nxhcwzulk\ntjbjvefm\nwfxxndko\nykqydswm\nxexihoxm\niaqsolsy\nkjnherjw\ndtagrwkw\neqxoamoo\nztuyerzl\nypdwcvjn\niydqnaih\nekmjoplw\ndbctlhav\nznowycbz\nyvigzdzr\nzfdfimzy\nchtxbdbz\nsyqpswah\nxwbtnnae\nsgiumfoa\nirpzjuce\nrejvrhgk\njgwuwcyd\nhpejgjsm\nkvtvleno\nmdcsgemu\ntombqbcl\nqlhwwdbn\nelltzqpr\ntmcrjpzw\nrdexwdvq\nlpvdqkpb\nsblwoucv\nenhzblxm\nbjuvkbvz\neuhyzmdx\nmkgflghc\naabcdkwr\nmdiksuzc\nmxgjblyu\nquhhkxgd\nfgwqnkba\ndvlmyqxh\nbdgmqcue\ndvpsxrxu\ngnddfjtv\nvyebxsui\nhtqhzeub\ngmwwjlwx\novcnnosg\nubzzoplu\nzyutatwp\nfvyeceuo\nkjtslrdl\ngimazmqa\nazwjgikh\nbjmsezgf\nqlwydcqb\nslkjjyjt\niermgjvf\nitktqmjg\nazaumrnj\nzpnkffmz\notpjumye\nomaijeay\nyddmqxle
        // Answer: qqqluigu
        //
        // --- Part Two ---
        //
        // Of course, that would be the message - if you hadn't agreed to use a modified repetition code instead.
        //
        // In this modified code, the sender instead transmits what looks like random data, but for each character, the character they actually want to send is
        // slightly less likely than the others. Even after signal-jamming noise, you can look at the letter distributions in each column and choose the least
        // common letter to reconstruct the original message.
        //
        // In the above example, the least common character in the first column is a; in the second, d, and so on.Repeating this process for the remaining
        // characters produces the original message, advent.
        //
        // Given the recording in your puzzle input and this new decoding methodology, what is the original message that Santa is trying to send?
        // Answer: lsoypmia
        public static string DecodeMessageWithMostOccuringLetter(string input)
        {
            return DecodeMessage(input, GetMostOccuring);
        }

        public static string DecodeMessageWithLeastOccuringLetter(string input)
        {
            return DecodeMessage(input, GetLeastOccuring);
        }

        private static string DecodeMessage(string input, Func<Dictionary<char, int>, char> extractChar)
        {
            var splitLines = input.Split("\n".ToCharArray());

            var passwordLength = splitLines[0].Length;

            var frequencies = new Dictionary<char, int>[passwordLength];

            for (int i = 0; i < passwordLength; i++)
            {
                frequencies[i] = new Dictionary<char, int>();
            }

            foreach (var line in splitLines)
            {
                for (int i = 0; i < passwordLength; i++)
                {
                    var character = line[i];

                    if (!frequencies[i].ContainsKey(character))
                    {
                        frequencies[i][character] = 0;
                    }

                    frequencies[i][character]++;
                }
            }

            var result = new StringBuilder(passwordLength);

            foreach (var frequency in frequencies)
            {
                result.Append(extractChar(frequency));
            }

            return result.ToString();
        }
        
        private static char GetMostOccuring(Dictionary<char, int> input)
        {
            var maxOccurances = int.MinValue;
            var result = ' ';

            foreach (var key in input.Keys)
            {
                var occurances = input[key];

                if (occurances > maxOccurances)
                {
                    maxOccurances = occurances;
                    result = key;
                }
            }

            return result;
        }

        private static char GetLeastOccuring(Dictionary<char, int> input)
        {
            var maxOccurances = int.MaxValue;
            var result = ' ';

            foreach (var key in input.Keys)
            {
                var occurances = input[key];

                if (occurances < maxOccurances)
                {
                    maxOccurances = occurances;
                    result = key;
                }
            }

            return result;
        }
    }
}
