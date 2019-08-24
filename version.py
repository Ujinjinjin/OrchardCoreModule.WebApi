import subprocess
import sys
import re
from typing import List

version_regex = r'(\d+)\.(\d+)\.(\d+)(-\w+)?'


def get_package_info(_target_name: str) -> List[str]:
    """Retrieve package info from nuget"""
    out = subprocess.Popen(['nuget', 'list', 'id:{0}'.format(_target_name), '-Prerelease'],
                           stdout=subprocess.PIPE,
                           stderr=subprocess.STDOUT)

    stdout, stderr = out.communicate()
    return stdout.decode('utf8').split('\r\n')


def get_next_version(_current_version: str) -> str:
    """Get next version number of package"""
    major, minor, patch, flag = re.findall(version_regex, _current_version)[0]
    patch = int(patch) + 1
    return '{0}.{1}.{2}{3}'.format(major, minor, patch, flag)


if __name__ == '__main__':
    assert len(sys.argv) != 1, 'You must specify only package name in order to get it\'s latest version number'
    target_name = sys.argv[1]

    print(sys.argv)

    package_infos = get_package_info(target_name)

    for package_info in package_infos:
        if len(package_info) == 0:
            continue
        name, version = package_info.split(' ')
        if name == target_name:
            next_version = get_next_version(version)
            print(next_version)
