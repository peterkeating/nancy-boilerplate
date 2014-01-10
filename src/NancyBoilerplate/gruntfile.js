/**
 * Unfamiliar with Grunt? Here are some links that will allow you to learn more.
 *
 * - http://gruntjs.com
 * - http://gruntjs.com/getting-started
 * - http://howtonode.org/introduction-to-npm
 */

'use strict';

module.exports = function(grunt) {

    /**
     * Configuring the tasks available for the build process.
     */
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        /**
         * Analyses JavaScript files using JSHint for errors or potential problems.
         * You can customise the parameters by modifying the .jshintrc file.
         * - http://jshint.com/
         */
        jshint: {
            all: [
                'gruntfile.js',
                'assets/js/**/*.js',
                '!assets/js/vendor/**/*.js'
            ],
            options: {
                jshintrc: '.jshintrc'
            }
        },

        /**
         * Optimizes the JavaScript into a single file using the r.js optimizer.
         */
        requirejs: {
            compile: {
                options: {
                    name: 'main',
                    mainConfigFile: 'assets/js/config.js',
                    paths: {
                        requireJS: 'vendor/require'
                    },
                    include: 'requireJS',
                    preserveLicenseComments: false,
                    out: 'assets/js/js-<%= pkg.version %>.js'
                }
            }
        },

        /**
         * Compiles Sass into a CSS file.
         */
        sass: {
            dev: {
                files: {
                    'assets/css/styles.css': 'assets/css/styles.scss'
                }
            },

            /**
             * Creates a compressed version of the project style sheet.
             */
            dist: {
                options: {
                    style: 'compressed'
                },
                files: {
                    'assets/css/css-<%= pkg.version %>.css': 'assets/css/styles.scss'
                }
            }
        },

        /**
         * Updates the URLs to assets in the mark-up files.
         */
        'string-replace': {
            dist: {
                files: {
                    'Views/Shared/_Layout.cshtml': 'Views/Shared/_Layout.cshtml'
                },
                options: {
                    replacements: [

                        /**
                         * All the JS is now in a single file to reduce HTTP requests so
                         * the AMD loading via requireJS can be replaced by a traditional
                         * loading a single JS file.
                         */
                        {
                            pattern: '<script data-main="assets/js/config" src="assets/js/vendor/require.js"></script>',
                            replacement: '<script src="assets/js/js-<%= pkg.version %>.js"></script>'
                        },

                        /**
                         * Updates style sheet declaration to load in the cache
                         * busting CSS file to ensure the visiting user has the
                         * latest styles.
                         */
                        {
                            pattern: '<link rel="stylesheet" href="assets/css/styles.css" />',
                            replacement: '<link rel="stylesheet" href="assets/css/css-<%= pkg.version %>.css" />'
                        }
                    ]
                }
            }
        },

        watch: {
            /**
             * Any changes made to a .scss file in the project trigger a conversion
             * of Sass to CSS.
             */
            sass: {
                files: 'assets/**/*.scss',
                tasks: ['sass:dev']
            },

            /**
             * Any changes to JavaScript files triggers the test suite to be tested.
             */
            js: {
                files: 'assets/**/*.js',
                tasks: ['jshint']
            }
        }
    });

    /**
     * Loads libraries to assist with the build process.
     */
    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-requirejs');
    grunt.loadNpmTasks('grunt-string-replace');
    grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.registerTask('default', ['sass:dev', 'jshint', 'watch']);

    /**
     * Organises the front end assets ready for a production environment.
     */
    grunt.registerTask('dist', ['jshint', 'sass:dist', 'requirejs', 'string-replace']);
};
