import sys
from collections import deque


def part1():
    PLAYER_START_HP = 50
    BOSS_START_HP = 55
    PLAYER_START_MANA = 500
    BOSS_DAMAGE = 8

    min_mana = sys.maxsize

    # PLAYER_HP = 0
    # BOSS_HP = 1
    # PLAYER_MANA = 2
    # SHIELD_TURNS = 3
    # POISON_TURNS = 4
    # RECHARGE_TURNS = 5
    # MANA_SPENT = 6
    queue = deque()
    queue.append((PLAYER_START_HP, BOSS_START_HP, PLAYER_START_MANA, 0, 0, 0, 0, 0))

    while len(queue) > 0:
        print(queue[0])
        player_hp, boss_hp, player_mana, shield_turns, poison_turns, recharge_turns, mana_spent, turn = queue.pop()

        if poison_turns > 0:
            boss_hp -= 3
            poison_turns -= 1

        if recharge_turns > 0:
            player_mana += 101
            recharge_turns -= 1

        has_shield = shield_turns > 0
        if shield_turns > 0:
            shield_turns -= 1

        # Game won
        if player_hp > 0 >= boss_hp:
            if mana_spent < min_mana:
                min_mana = mana_spent

            continue

        # Spent more mana than best solution found so far or player died
        if mana_spent > min_mana or player_hp <= 0:
            continue

        if turn == 1:
            turn = 0
            shield_value = 7 if has_shield else 0
            boss_damage = max(1, BOSS_DAMAGE - shield_value)
            queue.append((player_hp - boss_damage, boss_hp, player_mana, shield_turns, poison_turns, recharge_turns, mana_spent, turn))
            continue

        turn = 1

        # queue.append((player_hp, boss_hp, player_mana, shield_turns, poison_turns, recharge_turns, mana_spent, turn))

        # Magic missile
        if player_mana >= 53:
            queue.append((player_hp, boss_hp - 4, player_mana - 53, shield_turns, poison_turns, recharge_turns, mana_spent + 53, turn))

        # Drain
        if player_mana >= 73:
            queue.append((player_hp + 2, boss_hp - 2, player_mana - 73, shield_turns, poison_turns, recharge_turns, mana_spent + 73, turn))

        # Shield
        if player_mana >= 113 and shield_turns == 0:
            queue.append((player_hp, boss_hp, player_mana - 113, 6, poison_turns, recharge_turns, mana_spent + 113, turn))

        # Poison
        if player_mana >= 173 and poison_turns == 0:
            queue.append((player_hp, boss_hp, player_mana - 173, shield_turns, 6, recharge_turns, mana_spent + 173, turn))

        # Recharge
        if player_mana >= 229 and recharge_turns == 0:
            queue.append((player_hp, boss_hp, player_mana - 229, shield_turns, poison_turns, 5, mana_spent + 229, turn))

    print(min_mana)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()