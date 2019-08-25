import sys

from tools.lib.version.version_manager import VersionManager
from tools.lib.models.mode import ModeParser
from tools.lib.models.argv import ArgvTools, Argv

if __name__ == '__main__':
    argv: Argv = ArgvTools.parse_args(sys.argv[1:])
    mode = ModeParser(argv).parse()

    print(VersionManager(mode).get_next_version())
