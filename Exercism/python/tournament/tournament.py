def tally(rows):
    wins = {}
    draws = {}
    losses = {}
    teams = set()

    for row in rows:
        parts = row.split(";")
        teams.add(parts[0])
        teams.add(parts[1])

        if parts[2] == "win":
            increment_value(wins, parts[0])
            increment_value(losses, parts[1])
        elif parts[2] == "draw":
            increment_value(draws, parts[0])
            increment_value(draws, parts[1])
        elif parts[2] == "loss":
            increment_value(losses, parts[0])
            increment_value(wins, parts[1])
        else:
            raise ValueError("Invalid result" + row)

    results = []

    for team in sorted(teams):
        team_wins = get_value(wins, team)
        team_draws = get_value(draws, team)
        team_losses = get_value(losses, team)
        matches_played = team_wins + team_draws + team_losses
        points = 3 * team_wins + team_draws
        formatted_line = "{:31}|  {} |  {} |  {} |  {} |  {}" \
            .format(team, matches_played, team_wins, team_draws, team_losses, points)

        results.append((points, team, formatted_line))

    results = list(map(lambda x: x[2], sorted(results, key=lambda x: x[0], reverse=True)))

    result = [
        "Team                           | MP |  W |  D |  L |  P"
    ]

    result.extend(results)

    return result


def increment_value(dictionary, value):
    if value in dictionary:
        dictionary[value] += 1
    else:
        dictionary[value] = 1


def get_value(dictionary, value):
    if value in dictionary:
        return dictionary[value]

    return 0
