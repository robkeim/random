from collections import defaultdict


def tally(rows):
    wins = defaultdict(int)
    draws = defaultdict(int)
    losses = defaultdict(int)
    teams = set()

    for row in rows:
        team1, team2, result = row.split(";")
        teams.add(team1)
        teams.add(team2)

        if result == "win":
            wins[team1] += 1
            losses[team2] += 1
        elif result == "draw":
            draws[team1] += 1
            draws[team2] += 1
        elif result == "loss":
            losses[team1] += 1
            wins[team2] += 1
        else:
            raise ValueError("Invalid result" + row)

    results = []

    for team in teams:
        team_wins = wins[team]
        team_draws = draws[team]
        team_losses = losses[team]
        matches_played = team_wins + team_draws + team_losses
        points = 3 * team_wins + team_draws
        formatted_line = "{:31}|  {} |  {} |  {} |  {} |  {}" \
            .format(team, matches_played, team_wins, team_draws, team_losses, points)

        results.append((points, team, formatted_line))

    result = [
        "Team                           | MP |  W |  D |  L |  P"
    ]

    result.extend([x[2] for x in sorted(results, key=lambda x: (-x[0], x[1]))])

    return result
