using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day07
    {
        // Sample
        //private const string INPUT = "123 -> x\n456 -> y\nx AND y -> d\nx OR y -> e\nx LSHIFT 2 -> f\ny RSHIFT 2 -> g\nNOT x -> h\nNOT y -> i";

        // Part 1
        //private const string INPUT = "af AND ah -> ai\nNOT lk -> ll\nhz RSHIFT 1 -> is\nNOT go -> gp\ndu OR dt -> dv\nx RSHIFT 5 -> aa\nat OR az -> ba\neo LSHIFT 15 -> es\nci OR ct -> cu\nb RSHIFT 5 -> f\nfm OR fn -> fo\nNOT ag -> ah\nv OR w -> x\ng AND i -> j\nan LSHIFT 15 -> ar\n1 AND cx -> cy\njq AND jw -> jy\niu RSHIFT 5 -> ix\ngl AND gm -> go\nNOT bw -> bx\njp RSHIFT 3 -> jr\nhg AND hh -> hj\nbv AND bx -> by\ner OR es -> et\nkl OR kr -> ks\net RSHIFT 1 -> fm\ne AND f -> h\nu LSHIFT 1 -> ao\nhe RSHIFT 1 -> hx\neg AND ei -> ej\nbo AND bu -> bw\ndz OR ef -> eg\ndy RSHIFT 3 -> ea\ngl OR gm -> gn\nda LSHIFT 1 -> du\nau OR av -> aw\ngj OR gu -> gv\neu OR fa -> fb\nlg OR lm -> ln\ne OR f -> g\nNOT dm -> dn\nNOT l -> m\naq OR ar -> as\ngj RSHIFT 5 -> gm\nhm AND ho -> hp\nge LSHIFT 15 -> gi\njp RSHIFT 1 -> ki\nhg OR hh -> hi\nlc LSHIFT 1 -> lw\nkm OR kn -> ko\neq LSHIFT 1 -> fk\n1 AND am -> an\ngj RSHIFT 1 -> hc\naj AND al -> am\ngj AND gu -> gw\nko AND kq -> kr\nha OR gz -> hb\nbn OR by -> bz\niv OR jb -> jc\nNOT ac -> ad\nbo OR bu -> bv\nd AND j -> l\nbk LSHIFT 1 -> ce\nde OR dk -> dl\ndd RSHIFT 1 -> dw\nhz AND ik -> im\nNOT jd -> je\nfo RSHIFT 2 -> fp\nhb LSHIFT 1 -> hv\nlf RSHIFT 2 -> lg\ngj RSHIFT 3 -> gl\nki OR kj -> kk\nNOT ak -> al\nld OR le -> lf\nci RSHIFT 3 -> ck\n1 AND cc -> cd\nNOT kx -> ky\nfp OR fv -> fw\nev AND ew -> ey\ndt LSHIFT 15 -> dx\nNOT ax -> ay\nbp AND bq -> bs\nNOT ii -> ij\nci AND ct -> cv\niq OR ip -> ir\nx RSHIFT 2 -> y\nfq OR fr -> fs\nbn RSHIFT 5 -> bq\n0 -> c\n14146 -> b\nd OR j -> k\nz OR aa -> ab\ngf OR ge -> gg\ndf OR dg -> dh\nNOT hj -> hk\nNOT di -> dj\nfj LSHIFT 15 -> fn\nlf RSHIFT 1 -> ly\nb AND n -> p\njq OR jw -> jx\ngn AND gp -> gq\nx RSHIFT 1 -> aq\nex AND ez -> fa\nNOT fc -> fd\nbj OR bi -> bk\nas RSHIFT 5 -> av\nhu LSHIFT 15 -> hy\nNOT gs -> gt\nfs AND fu -> fv\ndh AND dj -> dk\nbz AND cb -> cc\ndy RSHIFT 1 -> er\nhc OR hd -> he\nfo OR fz -> ga\nt OR s -> u\nb RSHIFT 2 -> d\nNOT jy -> jz\nhz RSHIFT 2 -> ia\nkk AND kv -> kx\nga AND gc -> gd\nfl LSHIFT 1 -> gf\nbn AND by -> ca\nNOT hr -> hs\nNOT bs -> bt\nlf RSHIFT 3 -> lh\nau AND av -> ax\n1 AND gd -> ge\njr OR js -> jt\nfw AND fy -> fz\nNOT iz -> ja\nc LSHIFT 1 -> t\ndy RSHIFT 5 -> eb\nbp OR bq -> br\nNOT h -> i\n1 AND ds -> dt\nab AND ad -> ae\nap LSHIFT 1 -> bj\nbr AND bt -> bu\nNOT ca -> cb\nNOT el -> em\ns LSHIFT 15 -> w\ngk OR gq -> gr\nff AND fh -> fi\nkf LSHIFT 15 -> kj\nfp AND fv -> fx\nlh OR li -> lj\nbn RSHIFT 3 -> bp\njp OR ka -> kb\nlw OR lv -> lx\niy AND ja -> jb\ndy OR ej -> ek\n1 AND bh -> bi\nNOT kt -> ku\nao OR an -> ap\nia AND ig -> ii\nNOT ey -> ez\nbn RSHIFT 1 -> cg\nfk OR fj -> fl\nce OR cd -> cf\neu AND fa -> fc\nkg OR kf -> kh\njr AND js -> ju\niu RSHIFT 3 -> iw\ndf AND dg -> di\ndl AND dn -> do\nla LSHIFT 15 -> le\nfo RSHIFT 1 -> gh\nNOT gw -> gx\nNOT gb -> gc\nir LSHIFT 1 -> jl\nx AND ai -> ak\nhe RSHIFT 5 -> hh\n1 AND lu -> lv\nNOT ft -> fu\ngh OR gi -> gj\nlf RSHIFT 5 -> li\nx RSHIFT 3 -> z\nb RSHIFT 3 -> e\nhe RSHIFT 2 -> hf\nNOT fx -> fy\njt AND jv -> jw\nhx OR hy -> hz\njp AND ka -> kc\nfb AND fd -> fe\nhz OR ik -> il\nci RSHIFT 1 -> db\nfo AND fz -> gb\nfq AND fr -> ft\ngj RSHIFT 2 -> gk\ncg OR ch -> ci\ncd LSHIFT 15 -> ch\njm LSHIFT 1 -> kg\nih AND ij -> ik\nfo RSHIFT 3 -> fq\nfo RSHIFT 5 -> fr\n1 AND fi -> fj\n1 AND kz -> la\niu AND jf -> jh\ncq AND cs -> ct\ndv LSHIFT 1 -> ep\nhf OR hl -> hm\nkm AND kn -> kp\nde AND dk -> dm\ndd RSHIFT 5 -> dg\nNOT lo -> lp\nNOT ju -> jv\nNOT fg -> fh\ncm AND co -> cp\nea AND eb -> ed\ndd RSHIFT 3 -> df\ngr AND gt -> gu\nep OR eo -> eq\ncj AND cp -> cr\nlf OR lq -> lr\ngg LSHIFT 1 -> ha\net RSHIFT 2 -> eu\nNOT jh -> ji\nek AND em -> en\njk LSHIFT 15 -> jo\nia OR ig -> ih\ngv AND gx -> gy\net AND fe -> fg\nlh AND li -> lk\n1 AND io -> ip\nkb AND kd -> ke\nkk RSHIFT 5 -> kn\nid AND if -> ig\nNOT ls -> lt\ndw OR dx -> dy\ndd AND do -> dq\nlf AND lq -> ls\nNOT kc -> kd\ndy AND ej -> el\n1 AND ke -> kf\net OR fe -> ff\nhz RSHIFT 5 -> ic\ndd OR do -> dp\ncj OR cp -> cq\nNOT dq -> dr\nkk RSHIFT 1 -> ld\njg AND ji -> jj\nhe OR hp -> hq\nhi AND hk -> hl\ndp AND dr -> ds\ndz AND ef -> eh\nhz RSHIFT 3 -> ib\ndb OR dc -> dd\nhw LSHIFT 1 -> iq\nhe AND hp -> hr\nNOT cr -> cs\nlg AND lm -> lo\nhv OR hu -> hw\nil AND in -> io\nNOT eh -> ei\ngz LSHIFT 15 -> hd\ngk AND gq -> gs\n1 AND en -> eo\nNOT kp -> kq\net RSHIFT 5 -> ew\nlj AND ll -> lm\nhe RSHIFT 3 -> hg\net RSHIFT 3 -> ev\nas AND bd -> bf\ncu AND cw -> cx\njx AND jz -> ka\nb OR n -> o\nbe AND bg -> bh\n1 AND ht -> hu\n1 AND gy -> gz\nNOT hn -> ho\nck OR cl -> cm\nec AND ee -> ef\nlv LSHIFT 15 -> lz\nks AND ku -> kv\nNOT ie -> if\nhf AND hl -> hn\n1 AND r -> s\nib AND ic -> ie\nhq AND hs -> ht\ny AND ae -> ag\nNOT ed -> ee\nbi LSHIFT 15 -> bm\ndy RSHIFT 2 -> dz\nci RSHIFT 2 -> cj\nNOT bf -> bg\nNOT im -> in\nev OR ew -> ex\nib OR ic -> id\nbn RSHIFT 2 -> bo\ndd RSHIFT 2 -> de\nbl OR bm -> bn\nas RSHIFT 1 -> bl\nea OR eb -> ec\nln AND lp -> lq\nkk RSHIFT 3 -> km\nis OR it -> iu\niu RSHIFT 2 -> iv\nas OR bd -> be\nip LSHIFT 15 -> it\niw OR ix -> iy\nkk RSHIFT 2 -> kl\nNOT bb -> bc\nci RSHIFT 5 -> cl\nly OR lz -> ma\nz AND aa -> ac\niu RSHIFT 1 -> jn\ncy LSHIFT 15 -> dc\ncf LSHIFT 1 -> cz\nas RSHIFT 3 -> au\ncz OR cy -> da\nkw AND ky -> kz\nlx -> a\niw AND ix -> iz\nlr AND lt -> lu\njp RSHIFT 5 -> js\naw AND ay -> az\njc AND je -> jf\nlb OR la -> lc\nNOT cn -> co\nkh LSHIFT 1 -> lb\n1 AND jj -> jk\ny OR ae -> af\nck AND cl -> cn\nkk OR kv -> kw\nNOT cv -> cw\nkl AND kr -> kt\niu OR jf -> jg\nat AND az -> bb\njp RSHIFT 2 -> jq\niv AND jb -> jd\njn OR jo -> jp\nx OR ai -> aj\nba AND bc -> bd\njl OR jk -> jm\nb RSHIFT 1 -> v\no AND q -> r\nNOT p -> q\nk AND m -> n\nas RSHIFT 2 -> at";

        // Part 2
        private const string INPUT = "956 -> b\naf AND ah -> ai\nNOT lk -> ll\nhz RSHIFT 1 -> is\nNOT go -> gp\ndu OR dt -> dv\nx RSHIFT 5 -> aa\nat OR az -> ba\neo LSHIFT 15 -> es\nci OR ct -> cu\nb RSHIFT 5 -> f\nfm OR fn -> fo\nNOT ag -> ah\nv OR w -> x\ng AND i -> j\nan LSHIFT 15 -> ar\n1 AND cx -> cy\njq AND jw -> jy\niu RSHIFT 5 -> ix\ngl AND gm -> go\nNOT bw -> bx\njp RSHIFT 3 -> jr\nhg AND hh -> hj\nbv AND bx -> by\ner OR es -> et\nkl OR kr -> ks\net RSHIFT 1 -> fm\ne AND f -> h\nu LSHIFT 1 -> ao\nhe RSHIFT 1 -> hx\neg AND ei -> ej\nbo AND bu -> bw\ndz OR ef -> eg\ndy RSHIFT 3 -> ea\ngl OR gm -> gn\nda LSHIFT 1 -> du\nau OR av -> aw\ngj OR gu -> gv\neu OR fa -> fb\nlg OR lm -> ln\ne OR f -> g\nNOT dm -> dn\nNOT l -> m\naq OR ar -> as\ngj RSHIFT 5 -> gm\nhm AND ho -> hp\nge LSHIFT 15 -> gi\njp RSHIFT 1 -> ki\nhg OR hh -> hi\nlc LSHIFT 1 -> lw\nkm OR kn -> ko\neq LSHIFT 1 -> fk\n1 AND am -> an\ngj RSHIFT 1 -> hc\naj AND al -> am\ngj AND gu -> gw\nko AND kq -> kr\nha OR gz -> hb\nbn OR by -> bz\niv OR jb -> jc\nNOT ac -> ad\nbo OR bu -> bv\nd AND j -> l\nbk LSHIFT 1 -> ce\nde OR dk -> dl\ndd RSHIFT 1 -> dw\nhz AND ik -> im\nNOT jd -> je\nfo RSHIFT 2 -> fp\nhb LSHIFT 1 -> hv\nlf RSHIFT 2 -> lg\ngj RSHIFT 3 -> gl\nki OR kj -> kk\nNOT ak -> al\nld OR le -> lf\nci RSHIFT 3 -> ck\n1 AND cc -> cd\nNOT kx -> ky\nfp OR fv -> fw\nev AND ew -> ey\ndt LSHIFT 15 -> dx\nNOT ax -> ay\nbp AND bq -> bs\nNOT ii -> ij\nci AND ct -> cv\niq OR ip -> ir\nx RSHIFT 2 -> y\nfq OR fr -> fs\nbn RSHIFT 5 -> bq\n0 -> c\nd OR j -> k\nz OR aa -> ab\ngf OR ge -> gg\ndf OR dg -> dh\nNOT hj -> hk\nNOT di -> dj\nfj LSHIFT 15 -> fn\nlf RSHIFT 1 -> ly\nb AND n -> p\njq OR jw -> jx\ngn AND gp -> gq\nx RSHIFT 1 -> aq\nex AND ez -> fa\nNOT fc -> fd\nbj OR bi -> bk\nas RSHIFT 5 -> av\nhu LSHIFT 15 -> hy\nNOT gs -> gt\nfs AND fu -> fv\ndh AND dj -> dk\nbz AND cb -> cc\ndy RSHIFT 1 -> er\nhc OR hd -> he\nfo OR fz -> ga\nt OR s -> u\nb RSHIFT 2 -> d\nNOT jy -> jz\nhz RSHIFT 2 -> ia\nkk AND kv -> kx\nga AND gc -> gd\nfl LSHIFT 1 -> gf\nbn AND by -> ca\nNOT hr -> hs\nNOT bs -> bt\nlf RSHIFT 3 -> lh\nau AND av -> ax\n1 AND gd -> ge\njr OR js -> jt\nfw AND fy -> fz\nNOT iz -> ja\nc LSHIFT 1 -> t\ndy RSHIFT 5 -> eb\nbp OR bq -> br\nNOT h -> i\n1 AND ds -> dt\nab AND ad -> ae\nap LSHIFT 1 -> bj\nbr AND bt -> bu\nNOT ca -> cb\nNOT el -> em\ns LSHIFT 15 -> w\ngk OR gq -> gr\nff AND fh -> fi\nkf LSHIFT 15 -> kj\nfp AND fv -> fx\nlh OR li -> lj\nbn RSHIFT 3 -> bp\njp OR ka -> kb\nlw OR lv -> lx\niy AND ja -> jb\ndy OR ej -> ek\n1 AND bh -> bi\nNOT kt -> ku\nao OR an -> ap\nia AND ig -> ii\nNOT ey -> ez\nbn RSHIFT 1 -> cg\nfk OR fj -> fl\nce OR cd -> cf\neu AND fa -> fc\nkg OR kf -> kh\njr AND js -> ju\niu RSHIFT 3 -> iw\ndf AND dg -> di\ndl AND dn -> do\nla LSHIFT 15 -> le\nfo RSHIFT 1 -> gh\nNOT gw -> gx\nNOT gb -> gc\nir LSHIFT 1 -> jl\nx AND ai -> ak\nhe RSHIFT 5 -> hh\n1 AND lu -> lv\nNOT ft -> fu\ngh OR gi -> gj\nlf RSHIFT 5 -> li\nx RSHIFT 3 -> z\nb RSHIFT 3 -> e\nhe RSHIFT 2 -> hf\nNOT fx -> fy\njt AND jv -> jw\nhx OR hy -> hz\njp AND ka -> kc\nfb AND fd -> fe\nhz OR ik -> il\nci RSHIFT 1 -> db\nfo AND fz -> gb\nfq AND fr -> ft\ngj RSHIFT 2 -> gk\ncg OR ch -> ci\ncd LSHIFT 15 -> ch\njm LSHIFT 1 -> kg\nih AND ij -> ik\nfo RSHIFT 3 -> fq\nfo RSHIFT 5 -> fr\n1 AND fi -> fj\n1 AND kz -> la\niu AND jf -> jh\ncq AND cs -> ct\ndv LSHIFT 1 -> ep\nhf OR hl -> hm\nkm AND kn -> kp\nde AND dk -> dm\ndd RSHIFT 5 -> dg\nNOT lo -> lp\nNOT ju -> jv\nNOT fg -> fh\ncm AND co -> cp\nea AND eb -> ed\ndd RSHIFT 3 -> df\ngr AND gt -> gu\nep OR eo -> eq\ncj AND cp -> cr\nlf OR lq -> lr\ngg LSHIFT 1 -> ha\net RSHIFT 2 -> eu\nNOT jh -> ji\nek AND em -> en\njk LSHIFT 15 -> jo\nia OR ig -> ih\ngv AND gx -> gy\net AND fe -> fg\nlh AND li -> lk\n1 AND io -> ip\nkb AND kd -> ke\nkk RSHIFT 5 -> kn\nid AND if -> ig\nNOT ls -> lt\ndw OR dx -> dy\ndd AND do -> dq\nlf AND lq -> ls\nNOT kc -> kd\ndy AND ej -> el\n1 AND ke -> kf\net OR fe -> ff\nhz RSHIFT 5 -> ic\ndd OR do -> dp\ncj OR cp -> cq\nNOT dq -> dr\nkk RSHIFT 1 -> ld\njg AND ji -> jj\nhe OR hp -> hq\nhi AND hk -> hl\ndp AND dr -> ds\ndz AND ef -> eh\nhz RSHIFT 3 -> ib\ndb OR dc -> dd\nhw LSHIFT 1 -> iq\nhe AND hp -> hr\nNOT cr -> cs\nlg AND lm -> lo\nhv OR hu -> hw\nil AND in -> io\nNOT eh -> ei\ngz LSHIFT 15 -> hd\ngk AND gq -> gs\n1 AND en -> eo\nNOT kp -> kq\net RSHIFT 5 -> ew\nlj AND ll -> lm\nhe RSHIFT 3 -> hg\net RSHIFT 3 -> ev\nas AND bd -> bf\ncu AND cw -> cx\njx AND jz -> ka\nb OR n -> o\nbe AND bg -> bh\n1 AND ht -> hu\n1 AND gy -> gz\nNOT hn -> ho\nck OR cl -> cm\nec AND ee -> ef\nlv LSHIFT 15 -> lz\nks AND ku -> kv\nNOT ie -> if\nhf AND hl -> hn\n1 AND r -> s\nib AND ic -> ie\nhq AND hs -> ht\ny AND ae -> ag\nNOT ed -> ee\nbi LSHIFT 15 -> bm\ndy RSHIFT 2 -> dz\nci RSHIFT 2 -> cj\nNOT bf -> bg\nNOT im -> in\nev OR ew -> ex\nib OR ic -> id\nbn RSHIFT 2 -> bo\ndd RSHIFT 2 -> de\nbl OR bm -> bn\nas RSHIFT 1 -> bl\nea OR eb -> ec\nln AND lp -> lq\nkk RSHIFT 3 -> km\nis OR it -> iu\niu RSHIFT 2 -> iv\nas OR bd -> be\nip LSHIFT 15 -> it\niw OR ix -> iy\nkk RSHIFT 2 -> kl\nNOT bb -> bc\nci RSHIFT 5 -> cl\nly OR lz -> ma\nz AND aa -> ac\niu RSHIFT 1 -> jn\ncy LSHIFT 15 -> dc\ncf LSHIFT 1 -> cz\nas RSHIFT 3 -> au\ncz OR cy -> da\nkw AND ky -> kz\nlx -> a\niw AND ix -> iz\nlr AND lt -> lu\njp RSHIFT 5 -> js\naw AND ay -> az\njc AND je -> jf\nlb OR la -> lc\nNOT cn -> co\nkh LSHIFT 1 -> lb\n1 AND jj -> jk\ny OR ae -> af\nck AND cl -> cn\nkk OR kv -> kw\nNOT cv -> cw\nkl AND kr -> kt\niu OR jf -> jg\nat AND az -> bb\njp RSHIFT 2 -> jq\niv AND jb -> jd\njn OR jo -> jp\nx OR ai -> aj\nba AND bc -> bd\njl OR jk -> jm\nb RSHIFT 1 -> v\no AND q -> r\nNOT p -> q\nk AND m -> n\nas RSHIFT 2 -> at";
        private static readonly Dictionary<string, ushort> _values = new Dictionary<string, ushort>();
        private static readonly Regex _setValueRegex = new Regex(@"^([a-z0-9]+) -> ([a-z]+)$");
        private static readonly Regex _notRegex = new Regex(@"^NOT ([a-z]+) -> ([a-z]+)$");
        private static readonly Regex _twoOperandRegex = new Regex(@"^([a-z0-9]+) (AND|OR|LSHIFT|RSHIFT) ([a-z0-9]+) -> ([a-z]+)$");

        // Part 1: 956
        // Part 2: 40149
        public static void Part1()
        {
            var lines = INPUT.Split("\n".ToCharArray()).ToArray();
            var index = -1;

            while (lines.Length > 0)
            {
                index++;
                index %= lines.Length;

                var line = lines[index];

                var match = _setValueRegex.Match(line);

                if (match.Success)
                {
                    var parsedValue = ParseValue(match.Groups[1]);

                    if (parsedValue.HasValue)
                    {
                        _values[match.Groups[2].ToString()] = parsedValue.Value;
                        lines = lines.Where(l => l != line).ToArray();
                    }

                    continue;
                }

                match = _notRegex.Match(line);

                if (match.Success)
                {
                    var parsedValue = ParseValue(match.Groups[1]);

                    if (parsedValue.HasValue)
                    {
                        _values[match.Groups[2].ToString()] = (ushort)(~parsedValue.Value);
                        lines = lines.Where(l => l != line).ToArray();
                    }

                    continue;
                }

                match = _twoOperandRegex.Match(line);

                if (match.Success)
                {
                    var parsedValue1 = ParseValue(match.Groups[1]);
                    var parsedValue2 = ParseValue(match.Groups[3]);

                    if (parsedValue1.HasValue && parsedValue2.HasValue)
                    {
                        ushort result = 0;

                        switch (match.Groups[2].ToString())
                        {
                            case "AND":
                                result = (ushort)(parsedValue1.Value & parsedValue2.Value);
                                break;
                            case "LSHIFT":
                                result = (ushort)(parsedValue1.Value << parsedValue2.Value);
                                break;
                            case "OR":
                                result = (ushort)(parsedValue1.Value | parsedValue2.Value);
                                break;
                            case "RSHIFT":
                                result = (ushort)(parsedValue1.Value >> parsedValue2.Value);
                                break;
                            default:
                                throw new Exception("Invalid argument");
                        }
                        
                        _values[match.Groups[4].ToString()] = result;

                        lines = lines.Where(l => l != line).ToArray();
                    }

                    continue;
                }

                if (!match.Success)
                {
                    throw new Exception($"No matching instruction for line: {line}");
                }
            }

            foreach (var kvp in _values.OrderBy(kvp => kvp.Key))
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }

        private static ushort? ParseValue(Group group)
        {
            var operand = group.ToString();

            if (ushort.TryParse(operand, out ushort result))
            {
                return result;
            }

            if (_values.ContainsKey(operand))
            {
                return _values[operand];
            }

            return null;
        }
    }
}
