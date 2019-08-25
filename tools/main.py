import sys

from lib.version.version_manager import VersionManager
from lib.models.mode import ModeParser
from lib.models.argv import ArgvTools, Argv

if __name__ == '__main__':
    argv: Argv = ArgvTools.parse_args(sys.argv[1:])
    mode = ModeParser(argv).parse()

    print(VersionManager(mode).get_next_version())
