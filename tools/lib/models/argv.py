from dataclasses import dataclass, field
from typing import List, Dict

__all__ = ('Argv', 'ArgvTools')


@dataclass(init=True, repr=True)
class Argv:
    positional: List[str] = field(default_factory=list)
    kwargs: Dict[str, str] = field(default_factory=dict)
    flags: Dict[str, bool] = field(default_factory=dict)


class ArgvTools:
    @staticmethod
    def parse_args(argv: List[str]) -> Argv:
        result: Argv = Argv()
        for index, arg in enumerate(argv):
            if arg[0] == '-' and index + 1 < len(argv) and argv[index + 1][0] != '-':
                result.kwargs[arg] = argv[index + 1]
            elif arg[0] != '-' and index > 0 and argv[index - 1][0] == '-':
                continue
            elif arg[0] == '-' and ((index + 1 < len(argv) and argv[index + 1][0] == '-') or index + 1 == len(argv)):
                result.flags[arg] = True
            else:
                result.positional.append(arg)
        return result
